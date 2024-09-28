using DesafioMottu.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace DesafioMottu.Infrastructure.Repositories;

internal sealed class MotoRepository : Repository<Vehicle>, IVehicleRepository
{
    public MotoRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<Vehicle?> GetByLicensePlateAsync(LicensePlate licensePlate, CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<Vehicle>()
            .FirstOrDefaultAsync(user => user.LicensePlate == licensePlate, cancellationToken);
    }
}
