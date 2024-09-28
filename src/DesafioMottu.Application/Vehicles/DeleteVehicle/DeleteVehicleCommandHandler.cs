using DesafioMottu.Application.Abstractions.Messaging;
using DesafioMottu.Domain.Abstractions;
using DesafioMottu.Domain.Rentals;
using DesafioMottu.Domain.Vehicles;

namespace DesafioMottu.Application.Motorcycles.DeleteVehicle;

internal sealed class DeleteVehicleCommandHandler : ICommandHandler<DeleteVehicleCommand, Guid>
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IRentalRepository _locacaoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteVehicleCommandHandler(
        IVehicleRepository motoRepository,
        IRentalRepository locacaoRepository,
        IUnitOfWork unitOfWork)
    {
        _vehicleRepository = motoRepository;
        _locacaoRepository = locacaoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
    {
        Vehicle? vehicle = await _vehicleRepository.GetByIdAsync(request.MotoId, cancellationToken);

        if (vehicle is null)
        {
            return Result.Failure<Guid>(VehicleErrors.NotFound);
        }

        List<Rental> locacoes = await _locacaoRepository.GetByMotoIdAsync(request.MotoId, cancellationToken);

        if (locacoes.Any())
        {
            return Result.Failure<Guid>(VehicleErrors.DeleteAlreadyRented);
        }

        _vehicleRepository.Delete(vehicle);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return vehicle.Id;
    }
}
