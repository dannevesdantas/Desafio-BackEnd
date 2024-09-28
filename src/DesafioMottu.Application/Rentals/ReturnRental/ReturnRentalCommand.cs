using DesafioMottu.Application.Abstractions.Messaging;

namespace DesafioMottu.Application.Rentals.ReturnRental;

public sealed record ReturnRentalCommand(Guid LocacaoId, DateTime ReturnDate) : ICommand<Guid>;
