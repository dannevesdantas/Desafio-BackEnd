using DesafioMottu.Application.Abstractions.Messaging;
using DesafioMottu.Domain.Abstractions;
using DesafioMottu.Domain.Rentals;
using DesafioMottu.Domain.Vehicles;

namespace DesafioMottu.Application.Vehicles.DeleteVehicle;

internal sealed class DeleteVehicleCommandHandler : ICommandHandler<DeleteVehicleCommand, Guid>
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IRentalRepository _rentalRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteVehicleCommandHandler(
        IVehicleRepository vehicleRepository,
        IRentalRepository rentalRepository,
        IUnitOfWork unitOfWork)
    {
        _vehicleRepository = vehicleRepository;
        _rentalRepository = rentalRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
    {
        Vehicle? vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId, cancellationToken);

        if (vehicle is null)
        {
            return Result.Failure<Guid>(VehicleErrors.NotFound);
        }

        List<Rental> rentals = await _rentalRepository.GetByVehicleIdAsync(request.VehicleId, cancellationToken);

        if (rentals.Any())
        {
            return Result.Failure<Guid>(VehicleErrors.DeleteAlreadyRented);
        }

        _vehicleRepository.Delete(vehicle);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return vehicle.Id;
    }
}
