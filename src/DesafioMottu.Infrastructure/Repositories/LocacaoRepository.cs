using DesafioMottu.Domain.Rentals;
using DesafioMottu.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace DesafioMottu.Infrastructure.Repositories;

internal sealed class LocacaoRepository : Repository<Rental>, IRentalRepository
{
    public LocacaoRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<List<Rental>> GetByMotoIdAsync(Guid motoId, CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<Rental>()
            .Where(locacao => locacao.MotoId == motoId)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> IsOverlappingAsync(
        Vehicle moto,
        DateRange duration,
        CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<Rental>()
            .AnyAsync(
                locacao =>
                    locacao.MotoId == moto.Id &&
                    locacao.Duration.Start <= duration.End &&
                    locacao.Duration.End >= duration.Start,
                cancellationToken);
    }
}
