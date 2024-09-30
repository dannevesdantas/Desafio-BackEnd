using DesafioMottu.Application.Abstractions.Caching;

namespace DesafioMottu.Application.Rentals.GetRental;

public sealed record GetRentalQuery(Guid RentalId) : ICachedQuery<RentalResponse>
{
    public string CacheKey => $"rental-{RentalId}";

    public TimeSpan? Expiration => null;
}
