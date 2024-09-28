using DesafioMottu.Application.Abstractions.Messaging;

namespace DesafioMottu.Application.Motorcycles.SearchVehicles;

public sealed record SearchVehicleByIdQuery(Guid MotoId) : IQuery<VehicleResponse>;
