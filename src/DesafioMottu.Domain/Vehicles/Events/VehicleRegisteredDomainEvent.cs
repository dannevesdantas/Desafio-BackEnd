using DesafioMottu.Domain.Abstractions;

namespace DesafioMottu.Domain.Vehicles.Events;

public sealed record VehicleRegisteredDomainEvent(Guid VehicleId) : IDomainEvent;
