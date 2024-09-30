using DesafioMottu.Application.Abstractions.Messaging;

namespace DesafioMottu.Application.Rentals.ReturnVehicle;

public sealed record ReturnVehicleCommand(Guid RentId, DateTime ReturnDate) : ICommand<Guid>;
