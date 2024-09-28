using DesafioMottu.Application.Abstractions.Messaging;

namespace DesafioMottu.Application.Motorcycles.UpdateVehicle;

public sealed record UpdateVehicleCommand(Guid MotoId, string LicensePlateNumber) : ICommand<Guid>;
