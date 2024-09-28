using DesafioMottu.Application.Abstractions.Messaging;
using DesafioMottu.Domain.Abstractions;
using DesafioMottu.Domain.Vehicles;

namespace DesafioMottu.Application.Motorcycles.RegisterVehicle;

internal sealed class RegisterVehicleCommandHandler : ICommandHandler<RegisterVehicleCommand, Guid>
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterVehicleCommandHandler(
        IVehicleRepository motoRepository,
        IUnitOfWork unitOfWork)
    {
        _vehicleRepository = motoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RegisterVehicleCommand request, CancellationToken cancellationToken)
    {
        Vehicle? existingMoto = await _vehicleRepository.GetByLicensePlateAsync(new LicensePlate(request.Placa), cancellationToken);

        if (existingMoto is not null)
        {
            return Result.Failure<Guid>(VehicleErrors.AlreadyExists);
        }

        var newMoto = Vehicle.Create(
            new Model(request.Modelo),
            request.Ano,
            new LicensePlate(request.Placa));

        _vehicleRepository.Add(newMoto);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return newMoto.Id;
    }
}
