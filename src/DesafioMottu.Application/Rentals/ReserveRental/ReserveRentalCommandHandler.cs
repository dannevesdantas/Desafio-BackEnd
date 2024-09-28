using DesafioMottu.Application.Abstractions.Clock;
using DesafioMottu.Application.Abstractions.Messaging;
using DesafioMottu.Application.Exceptions;
using DesafioMottu.Domain.Abstractions;
using DesafioMottu.Domain.DriversLicense;
using DesafioMottu.Domain.Rentals;
using DesafioMottu.Domain.Users;
using DesafioMottu.Domain.Vehicles;

namespace DesafioMottu.Application.Rentals.ReserveRental;

internal sealed class ReserveRentalCommandHandler : ICommandHandler<ReserveRentalCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IDriversLicenseRepository _driversLicenseRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IRentalRepository _locacaoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ReserveRentalCommandHandler(
        IUserRepository userRepository,
        IDriversLicenseRepository driversLicenseRepository,
        IVehicleRepository motoRepository,
        IRentalRepository locacaoRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _userRepository = userRepository;
        _driversLicenseRepository = driversLicenseRepository;
        _vehicleRepository = motoRepository;
        _locacaoRepository = locacaoRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<Guid>> Handle(ReserveRentalCommand request, CancellationToken cancellationToken)
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

        Vehicle? vehicle = await _vehicleRepository.GetByIdAsync(request.MotoId, cancellationToken);

        if (vehicle is null)
        {
            return Result.Failure<Guid>(VehicleErrors.NotFound);
        }

        var duration = DateRange.Create(request.StartDate, request.EndDate);

        if (await _locacaoRepository.IsOverlappingAsync(vehicle, duration, cancellationToken))
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
            Result<Domain.Rentals.Rental> locacaoResult = Domain.Rentals.Rental.Reserve(
                user.Id,
                user.DriversLicense,
                vehicle,
                duration,
                request.PredictedEndDate,
                plan.Value,
                _dateTimeProvider.UtcNow);

            if (locacaoResult.IsFailure)
            {
                return Result.Failure<Guid>(locacaoResult.Error);
            }

            _locacaoRepository.Add(locacaoResult.Value);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return locacaoResult.Value.Id;
        }
        catch (ConcurrencyException)
        {
            return Result.Failure<Guid>(RentalErrors.Overlap);
        }
    }
}
