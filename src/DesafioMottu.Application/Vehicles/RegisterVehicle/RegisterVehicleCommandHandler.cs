using DesafioMottu.Application.Abstractions.Messaging;
using DesafioMottu.Domain.Abstractions;
using DesafioMottu.Domain.Vehicles;

namespace DesafioMottu.Application.Vehicles.RegisterVehicle;

internal sealed class RegisterVehicleCommandHandler : ICommandHandler<RegisterVehicleCommand, Guid>
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterVehicleCommandHandler(
        IVehicleRepository vehicleRepository,
        IUnitOfWork unitOfWork)
    {
        _vehicleRepository = vehicleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RegisterVehicleCommand request, CancellationToken cancellationToken)
    {
        Vehicle? existingVehicle = await _vehicleRepository.GetByLicensePlateAsync(new LicensePlate(request.Placa), cancellationToken);

        if (existingVehicle is not null)
        {
            return Result.Failure<Guid>(VehicleErrors.AlreadyExists);
        }

        var newVehicle = Vehicle.Create(
            new Model(request.Modelo),
            request.Ano,
            new LicensePlate(request.Placa));

        _vehicleRepository.Add(newVehicle);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return newVehicle.Id;
    }
}
