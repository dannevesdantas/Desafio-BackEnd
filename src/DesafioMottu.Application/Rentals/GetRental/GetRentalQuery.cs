using DesafioMottu.Application.Abstractions.Caching;

namespace DesafioMottu.Application.Rentals.GetRental;

public sealed record GetRentalQuery(Guid LocacaoId) : ICachedQuery<RentalResponse>
{
    public string CacheKey => $"bookings-{LocacaoId}";

    public TimeSpan? Expiration => null;
}
