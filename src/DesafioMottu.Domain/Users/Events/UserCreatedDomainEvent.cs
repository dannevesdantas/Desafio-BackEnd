using DesafioMottu.Domain.Abstractions;

namespace DesafioMottu.Domain.Users.Events;

public sealed record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;
