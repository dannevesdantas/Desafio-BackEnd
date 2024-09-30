using DesafioMottu.Application.Abstractions.Messaging;

namespace DesafioMottu.Application.Rentals.GetRental;

public sealed record GetRentalQuery(Guid RentalId) : IQuery<RentalResponse>;
