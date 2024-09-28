using DesafioMottu.Application.Abstractions.Messaging;

namespace DesafioMottu.Application.Motorcycles.DeleteVehicle;

public sealed record DeleteVehicleCommand(Guid MotoId) : ICommand<Guid>;
