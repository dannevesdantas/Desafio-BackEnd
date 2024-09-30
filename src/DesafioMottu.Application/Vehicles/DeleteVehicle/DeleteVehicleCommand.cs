using DesafioMottu.Application.Abstractions.Messaging;

namespace DesafioMottu.Application.Vehicles.DeleteVehicle;

public sealed record DeleteVehicleCommand(Guid VehicleId) : ICommand<Guid>;
