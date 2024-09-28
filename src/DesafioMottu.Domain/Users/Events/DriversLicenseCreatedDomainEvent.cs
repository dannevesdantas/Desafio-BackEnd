using DesafioMottu.Domain.Abstractions;

namespace DesafioMottu.Domain.Users.Events;

public sealed record DriversLicenseCreatedDomainEvent(Guid DriversLicenseId) : IDomainEvent;
