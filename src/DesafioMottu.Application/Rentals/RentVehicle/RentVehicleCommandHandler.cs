using DesafioMottu.Application.Abstractions.Clock;
using DesafioMottu.Application.Abstractions.Messaging;
using DesafioMottu.Application.Exceptions;
using DesafioMottu.Domain.Abstractions;
using DesafioMottu.Domain.DriversLicense;
using DesafioMottu.Domain.Rentals;
using DesafioMottu.Domain.Users;
using DesafioMottu.Domain.Vehicles;

namespace DesafioMottu.Application.Rentals.RentVehicle;

internal sealed class RentVehicleCommandHandler : ICommandHandler<RentVehicleCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IDriversLicenseRepository _driversLicenseRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IRentalRepository _rentalRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public RentVehicleCommandHandler(
        IUserRepository userRepository,
        IDriversLicenseRepository driversLicenseRepository,
        IVehicleRepository vehicleRepository,
        IRentalRepository rentalRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _userRepository = userRepository;
        _driversLicenseRepository = driversLicenseRepository;
        _vehicleRepository = vehicleRepository;
        _rentalRepository = rentalRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<Guid>> Handle(RentVehicleCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }

        DriversLicense? driversLicense = await _driversLicenseRepository.GetByUserIdAsync(request.UserId, cancellationToken);

        if (driversLicense is null)
        {
            return Result.Failure<Guid>(UserErrors.Unlicensed);
        }

        Vehicle? vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId, cancellationToken);

        if (vehicle is null)
        {
            return Result.Failure<Guid>(VehicleErrors.NotFound);
        }

        var duration = DateRange.Create(request.StartDate, request.EndDate);

        if (await _rentalRepository.IsOverlappingAsync(vehicle, duration, cancellationToken))
        {
            return Result.Failure<Guid>(RentalErrors.Overlap);
        }

        Result<Plan> plan = Plan.Create(request.Plan);

        if (plan.IsFailure)
        {
            return Result.Failure<Guid>(plan.Error);
        }

        try
        {
            Result<Rental> rentalResult = Rental.Reserve(
                user.Id,
                user.DriversLicense,
                vehicle,
                duration,
                request.PredictedEndDate,
                plan.Value,
                _dateTimeProvider.UtcNow);

            if (rentalResult.IsFailure)
            {
                return Result.Failure<Guid>(rentalResult.Error);
            }

            _rentalRepository.Add(rentalResult.Value);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return rentalResult.Value.Id;
        }
        catch (ConcurrencyException)
        {
            return Result.Failure<Guid>(RentalErrors.Overlap);
        }
    }
}
