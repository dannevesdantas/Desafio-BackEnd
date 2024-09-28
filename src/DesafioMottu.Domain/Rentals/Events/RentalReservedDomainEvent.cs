using DesafioMottu.Domain.Abstractions;

namespace DesafioMottu.Domain.Rentals.Events;

public sealed record RentalReservedDomainEvent(Guid LocacaoId) : IDomainEvent;
