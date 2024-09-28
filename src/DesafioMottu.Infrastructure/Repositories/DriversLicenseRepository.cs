using DesafioMottu.Domain.DriversLicense;
using Microsoft.EntityFrameworkCore;

namespace DesafioMottu.Infrastructure.Repositories;

internal sealed class DriversLicenseRepository : Repository<DriversLicense>, IDriversLicenseRepository
{
    public DriversLicenseRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<DriversLicense?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await DbContext
        .Set<DriversLicense>()
            .FirstOrDefaultAsync(license => license.UserId == userId, cancellationToken);
    }

    public async Task<DriversLicense?> GetByNumberAsync(string number, CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<DriversLicense>()
            .FirstOrDefaultAsync(license => license.Number == number, cancellationToken);
    }

    public override void Add(DriversLicense user)
    {
        DbContext.Add(user);
    }
}
