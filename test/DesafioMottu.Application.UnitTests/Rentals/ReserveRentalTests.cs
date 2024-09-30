using DesafioMottu.Application.Abstractions.Clock;
using DesafioMottu.Application.Exceptions;
using DesafioMottu.Application.Rentals.RentVehicle;
using DesafioMottu.Application.UnitTests.Users;
using DesafioMottu.Application.UnitTests.Vehicles;
using DesafioMottu.Domain.Abstractions;
using DesafioMottu.Domain.DriversLicense;
using DesafioMottu.Domain.Rentals;
using DesafioMottu.Domain.UnitTests.DriversLicense;
using DesafioMottu.Domain.Users;
using DesafioMottu.Domain.Vehicles;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace DesafioMottu.Application.UnitTests.Rentals;

public class ReserveRentalTests
{
    private static readonly DateTime UtcNow = DateTime.UtcNow;
    private static readonly RentVehicleCommand Command = new(Guid.NewGuid(),
        Guid.NewGuid(),
        new DateTime(2024, 1, 1),
        new DateTime(2024, 1, 7),
        new DateTime(2024, 1, 7),
        7);

    private readonly RentVehicleCommandHandler _handler;
    private readonly IUserRepository _userRepositoryMock;
    private readonly IDriversLicenseRepository _driversLicenseRepositoryMock;
    private readonly IVehicleRepository _vehicleRepositoryMock;
    private readonly IRentalRepository _rentalRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public ReserveRentalTests()
    {
        _userRepositoryMock = Substitute.For<IUserRepository>();
        _driversLicenseRepositoryMock = Substitute.For<IDriversLicenseRepository>();
        _vehicleRepositoryMock = Substitute.For<IVehicleRepository>();
        _rentalRepositoryMock = Substitute.For<IRentalRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();

        IDateTimeProvider dateTimeProviderMock = Substitute.For<IDateTimeProvider>();
        dateTimeProviderMock.UtcNow.Returns(UtcNow);

        _handler = new RentVehicleCommandHandler(
            _userRepositoryMock,
            _driversLicenseRepositoryMock,
            _vehicleRepositoryMock,
            _rentalRepositoryMock,
            _unitOfWorkMock,
            dateTimeProviderMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenUserIsNull()
    {
        // Arrange
        _userRepositoryMock
            .GetByIdAsync(Command.UserId, Arg.Any<CancellationToken>())
            .Returns((User?)null);

        // Act
        Result<Guid> result = await _handler.Handle(Command, default);

        // Assert
        result.Error.Should().Be(UserErrors.NotFound);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenVehicleIsNull()
    {
        // Arrange
        User user = UserData.Create();
        DriversLicense driversLicense = DriversLicenseData.Create(user.Id);
        user.SetDriversLicense(driversLicense);

        _userRepositoryMock
            .GetByIdAsync(Command.UserId, Arg.Any<CancellationToken>())
            .Returns(user);

        _driversLicenseRepositoryMock
            .GetByUserIdAsync(Command.UserId, Arg.Any<CancellationToken>())
            .Returns(driversLicense);

        _vehicleRepositoryMock
            .GetByIdAsync(Command.VehicleId, Arg.Any<CancellationToken>())
            .Returns((Vehicle?)null);

        // Act
        Result<Guid> result = await _handler.Handle(Command, default);

        // Assert
        result.Error.Should().Be(VehicleErrors.NotFound);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenVehicleIsRented()
    {
        // Arrange
        User user = UserData.Create();
        DriversLicense driversLicense = DriversLicenseData.Create(user.Id);
        user.SetDriversLicense(driversLicense);
        Vehicle vehicle = VehicleData.Create();
        var duration = DateRange.Create(Command.StartDate, Command.EndDate);

        _userRepositoryMock
            .GetByIdAsync(Command.UserId, Arg.Any<CancellationToken>())
            .Returns(user);

        _driversLicenseRepositoryMock
            .GetByUserIdAsync(Command.UserId, Arg.Any<CancellationToken>())
            .Returns(driversLicense);

        _vehicleRepositoryMock
            .GetByIdAsync(Command.VehicleId, Arg.Any<CancellationToken>())
            .Returns(vehicle);

        _rentalRepositoryMock
            .IsOverlappingAsync(vehicle, duration, Arg.Any<CancellationToken>())
            .Returns(true);

        // Act
        Result<Guid> result = await _handler.Handle(Command, default);

        // Assert
        result.Error.Should().Be(RentalErrors.Overlap);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenUnitOfWorkThrows()
    {
        // Arrange
        User user = UserData.Create();
        DriversLicense driversLicense = DriversLicenseData.Create(user.Id);
        user.SetDriversLicense(driversLicense);
        Vehicle vehicle = VehicleData.Create();
        var duration = DateRange.Create(Command.StartDate, Command.EndDate);

        _userRepositoryMock
            .GetByIdAsync(Command.UserId, Arg.Any<CancellationToken>())
            .Returns(user);

        _driversLicenseRepositoryMock
            .GetByUserIdAsync(Command.UserId, Arg.Any<CancellationToken>())
            .Returns(driversLicense);

        _vehicleRepositoryMock
            .GetByIdAsync(Command.VehicleId, Arg.Any<CancellationToken>())
            .Returns(vehicle);

        _rentalRepositoryMock
            .IsOverlappingAsync(vehicle, duration, Arg.Any<CancellationToken>())
            .Returns(false);

        _unitOfWorkMock
            .SaveChangesAsync()
            .ThrowsAsync(new ConcurrencyException("Concurrency", new Exception()));

        // Act
        Result<Guid> result = await _handler.Handle(Command, default);

        // Assert
        result.Error.Should().Be(RentalErrors.Overlap);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenRented()
    {
        // Arrange
        User user = UserData.Create();
        DriversLicense driversLicense = DriversLicenseData.Create(user.Id);
        user.SetDriversLicense(driversLicense);
        Vehicle vehicle = VehicleData.Create();
        var duration = DateRange.Create(Command.StartDate, Command.EndDate);

        _userRepositoryMock
            .GetByIdAsync(Command.UserId, Arg.Any<CancellationToken>())
            .Returns(user);

        _driversLicenseRepositoryMock
            .GetByUserIdAsync(Command.UserId, Arg.Any<CancellationToken>())
            .Returns(driversLicense);

        _vehicleRepositoryMock
            .GetByIdAsync(Command.VehicleId, Arg.Any<CancellationToken>())
            .Returns(vehicle);

        _rentalRepositoryMock
            .IsOverlappingAsync(vehicle, duration, Arg.Any<CancellationToken>())
            .Returns(false);

        // Act
        Result<Guid> result = await _handler.Handle(Command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallRepository_WhenRented()
    {
        // Arrange
        User user = UserData.Create();
        DriversLicense driversLicense = DriversLicenseData.Create(user.Id);
        user.SetDriversLicense(driversLicense);
        Vehicle vehicle = VehicleData.Create();
        var duration = DateRange.Create(Command.StartDate, Command.EndDate);

        _userRepositoryMock
            .GetByIdAsync(Command.UserId, Arg.Any<CancellationToken>())
            .Returns(user);

        _driversLicenseRepositoryMock
            .GetByUserIdAsync(Command.UserId, Arg.Any<CancellationToken>())
            .Returns(driversLicense);

        _vehicleRepositoryMock
            .GetByIdAsync(Command.VehicleId, Arg.Any<CancellationToken>())
            .Returns(vehicle);
        _rentalRepositoryMock
            .IsOverlappingAsync(vehicle, duration, Arg.Any<CancellationToken>())
            .Returns(false);

        // Act
        Result<Guid> result = await _handler.Handle(Command, default);

        // Assert
        _rentalRepositoryMock.Received(1).Add(Arg.Is<Rental>(b => b.Id == result.Value));
    }
}
