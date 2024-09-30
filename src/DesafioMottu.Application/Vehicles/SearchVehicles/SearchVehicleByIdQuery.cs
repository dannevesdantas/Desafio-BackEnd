using DesafioMottu.Application.Abstractions.Messaging;

namespace DesafioMottu.Application.Vehicles.SearchVehicles;

public sealed record SearchVehicleByIdQuery(Guid VehicleId) : IQuery<VehicleResponse>;
