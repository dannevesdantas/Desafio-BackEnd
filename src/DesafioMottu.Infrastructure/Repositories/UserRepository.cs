using DesafioMottu.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace DesafioMottu.Infrastructure.Repositories;

internal sealed class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<User?> GetByCnpjAsync(Cnpj cnpj, CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<User>()
            .FirstOrDefaultAsync(user => user.Cnpj == cnpj, cancellationToken);
    }

    public override void Add(User user)
    {
        DbContext.Add(user);
    }
}
