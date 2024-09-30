using DesafioMottu.Domain.Abstractions;

namespace DesafioMottu.Domain.Rentals.Events;

public sealed record VehicleRentedDomainEvent(Guid RentalId) : IDomainEvent;
