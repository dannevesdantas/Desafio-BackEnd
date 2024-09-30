using DesafioMottu.Application.EventBus;
using DesafioMottu.Domain.Vehicles;
using DesafioMottu.Domain.Vehicles.Events;
using MediatR;
using Newtonsoft.Json;

namespace DesafioMottu.Application.Rentals.RentVehicle;

internal sealed class VehicleRegisteredDomainEventHandler : INotificationHandler<VehicleRegisteredDomainEvent>
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IEventBus _eventBus;

    public VehicleRegisteredDomainEventHandler(
        IVehicleRepository vehicleRepository,
        IEventBus eventBus)
    {
        _vehicleRepository = vehicleRepository;
        _eventBus = eventBus;
    }

    public async Task Handle(VehicleRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        Vehicle? vehicle = await _vehicleRepository.GetByIdAsync(notification.VehicleId, cancellationToken);

        if (vehicle is null)
        {
            return;
        }

        dynamic vehicleData = new { Id = vehicle.Id, Model = vehicle.Model.Value, Year = vehicle.Year, LicensePlateNumber = vehicle.LicensePlate.Number };

        await _eventBus.PublishAsync(JsonConvert.SerializeObject(vehicleData), cancellationToken);
    }
}
