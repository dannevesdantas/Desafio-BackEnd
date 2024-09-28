﻿using DesafioMottu.Domain.Abstractions;

namespace DesafioMottu.Domain.Rentals.Events;

public sealed record RentalReturnedDomainEvent(Guid LocacaoId) : IDomainEvent;
