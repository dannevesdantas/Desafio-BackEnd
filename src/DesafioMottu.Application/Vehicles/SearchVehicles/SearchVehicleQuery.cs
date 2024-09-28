using DesafioMottu.Application.Abstractions.Messaging;

namespace DesafioMottu.Application.Motorcycles.SearchVehicles;

public sealed record SearchVehicleQuery(string? LicensePlateNumber) : IQuery<IReadOnlyList<VehicleResponse>>;
