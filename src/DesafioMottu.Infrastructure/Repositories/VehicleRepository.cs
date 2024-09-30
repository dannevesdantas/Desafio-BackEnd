using DesafioMottu.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace DesafioMottu.Infrastructure.Repositories;

internal sealed class VehicleRepository : Repository<Vehicle>, IVehicleRepository
{
    public VehicleRepository(ApplicationDbContext dbContext)
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
