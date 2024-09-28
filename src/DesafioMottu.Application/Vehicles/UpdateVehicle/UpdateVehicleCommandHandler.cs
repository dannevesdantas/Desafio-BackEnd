using DesafioMottu.Application.Abstractions.Messaging;
using DesafioMottu.Domain.Abstractions;
using DesafioMottu.Domain.Vehicles;

namespace DesafioMottu.Application.Motorcycles.UpdateVehicle;

internal sealed class UpdateVehicleCommandHandler : ICommandHandler<UpdateVehicleCommand, Guid>
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateVehicleCommandHandler(
        IVehicleRepository motoRepository,
        IUnitOfWork unitOfWork)
    {
        _vehicleRepository = motoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
    {
        Vehicle? vehicle = await _vehicleRepository.GetByIdAsync(request.MotoId, cancellationToken);

        if (vehicle is null)
        {
            return Result.Failure<Guid>(VehicleErrors.NotFound);
        }

        Vehicle? motoWithSameLicensePlate = await _vehicleRepository.GetByLicensePlateAsync(new LicensePlate(request.LicensePlateNumber), cancellationToken);

        if (motoWithSameLicensePlate is not null)
        {
            return Result.Failure<Guid>(VehicleErrors.PlateAlreadyTaken);
        }

        vehicle.SetLicensePlateNumber(request.LicensePlateNumber);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return vehicle.Id;
    }
}
