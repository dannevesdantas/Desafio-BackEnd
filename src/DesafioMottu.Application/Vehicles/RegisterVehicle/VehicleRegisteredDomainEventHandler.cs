using DesafioMottu.Application.EventBus;
using DesafioMottu.Domain.Vehicles;
using DesafioMottu.Domain.Vehicles.Events;
using MediatR;

namespace DesafioMottu.Application.Rentals.ReserveRental;

internal sealed class VehicleRegisteredDomainEventHandler : INotificationHandler<VehicleRegisteredDomainEvent>
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IEventBus _eventBus;

    public VehicleRegisteredDomainEventHandler(
        IVehicleRepository motorcycleRepository,
        IEventBus eventBus)
    {
        _vehicleRepository = motorcycleRepository;
        _eventBus = eventBus;
    }

    public async Task Handle(VehicleRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        Vehicle? motorcycle = await _vehicleRepository.GetByIdAsync(notification.MotoId, cancellationToken);

        if (motorcycle is null)
        {
            return;
        }

        await _eventBus.PublishAsync(motorcycle, cancellationToken);
    }
}
