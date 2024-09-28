namespace DesafioMottu.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<User?> GetByCnpjAsync(Cnpj cnpj, CancellationToken cancellationToken = default);

    void Add(User user);
}
