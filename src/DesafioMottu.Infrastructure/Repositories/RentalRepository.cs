using DesafioMottu.Domain.Rentals;
using DesafioMottu.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace DesafioMottu.Infrastructure.Repositories;

internal sealed class RentalRepository : Repository<Rental>, IRentalRepository
{
    public RentalRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<List<Rental>> GetByVehicleIdAsync(Guid vehicleId, CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<Rental>()
            .Where(rental => rental.VehicleId == vehicleId)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> IsOverlappingAsync(
        Vehicle vehicle,
        DateRange duration,
        CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<Rental>()
            .AnyAsync(
                rental =>
                    rental.VehicleId == vehicle.Id &&
                    rental.Duration.Start <= duration.End &&
                    rental.Duration.End >= duration.Start,
                cancellationToken);
    }
}
