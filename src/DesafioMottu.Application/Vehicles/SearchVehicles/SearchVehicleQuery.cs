using DesafioMottu.Application.Abstractions.Messaging;

namespace DesafioMottu.Application.Vehicles.SearchVehicles;

public sealed record SearchVehicleQuery(string? LicensePlateNumber) : IQuery<IReadOnlyList<VehicleResponse>>;
