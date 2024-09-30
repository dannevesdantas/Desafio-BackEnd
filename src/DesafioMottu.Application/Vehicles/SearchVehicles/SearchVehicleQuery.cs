using DesafioMottu.Application.Abstractions.Caching;

namespace DesafioMottu.Application.Vehicles.SearchVehicles;

public sealed record SearchVehicleQuery(string? LicensePlateNumber) : ICachedQuery<IReadOnlyList<VehicleResponse>>
{
    public string CacheKey => $"vehicle-{LicensePlateNumber}";

    public TimeSpan? Expiration => null;
}
