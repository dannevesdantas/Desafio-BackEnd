using DesafioMottu.Domain.Abstractions;
using DesafioMottu.Domain.Rentals;
using DesafioMottu.Domain.Rentals.Events;
using DesafioMottu.Domain.UnitTests.Infrastructure;
using DesafioMottu.Domain.UnitTests.Users;
using DesafioMottu.Domain.UnitTests.Vehicles;
using DesafioMottu.Domain.Users;
using DesafioMottu.Domain.Vehicles;
using FluentAssertions;

namespace DesafioMottu.Domain.UnitTests.Rentals;

public class RentalTests : BaseTest
{
    [Fact]
    public void Rental_Should_RaiseVehicleRentedDomainEvent()
    {
        // Arrange
        var user = User.Create(UserData.Name, UserData.Cnpj, UserData.BirthDate);
        var driversLicense = Domain.DriversLicense.DriversLicense.Create(user.Id, "68234859262", new List<char>() { 'A' });
        user.SetDriversLicense(driversLicense);
        var duration = DateRange.Create(new DateTime(2024, 1, 1), new DateTime(2024, 1, 7));
        DateTime predictedEndDate = duration.End;
        Plan plan = Plan.Create(7).Value;
        Vehicle vehicle = VehicleData.Create();

        // Act
        var rentalResult = Rental.Reserve(user.Id, driversLicense, vehicle, duration, predictedEndDate, plan, DateTime.UtcNow);

        // Assert
        VehicleRentedDomainEvent vehicleRentedDomainEvent = AssertDomainEventWasPublished<VehicleRentedDomainEvent>(rentalResult.Value);

        vehicleRentedDomainEvent.RentalId.Should().Be(rentalResult.Value.Id);
    }

    [Fact]
    public void Rental_Should_ReturnFailure_WhenDriversLicenseNotMeetLegalRequirements()
    {
        // Arrange
        var user = User.Create(UserData.Name, UserData.Cnpj, UserData.BirthDate);
        // Somente entregadores habilitados na categoria A podem efetuar uma locação
        var driversLicense = Domain.DriversLicense.DriversLicense.Create(user.Id, "68234859262", new List<char>() { 'B' });
        user.SetDriversLicense(driversLicense);
        var duration = DateRange.Create(new DateTime(2024, 1, 1), new DateTime(2024, 1, 7));
        DateTime predictedEndDate = duration.End;
        Plan plan = Plan.Create(7).Value;
        Vehicle vehicle = VehicleData.Create();

        // Act
        Result<Rental> rentalResult = Rental.Reserve(user.Id, driversLicense, vehicle, duration, predictedEndDate, plan, DateTime.UtcNow);

        // Assert
        rentalResult.IsFailure.Should().BeTrue();
    }
}
