using DesafioMottu.Domain.Abstractions;

namespace DesafioMottu.Domain.Rentals.Events;

public sealed record VehicleReturnedDomainEvent(Guid RentalId) : IDomainEvent;
