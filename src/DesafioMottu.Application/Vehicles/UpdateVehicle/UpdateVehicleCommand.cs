using DesafioMottu.Application.Abstractions.Messaging;

namespace DesafioMottu.Application.Vehicles.UpdateVehicle;

public sealed record UpdateVehicleCommand(Guid VechicleId, string LicensePlateNumber) : ICommand<Guid>;
