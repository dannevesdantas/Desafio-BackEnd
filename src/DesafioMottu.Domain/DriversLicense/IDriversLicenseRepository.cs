namespace DesafioMottu.Domain.DriversLicense;

public interface IDriversLicenseRepository
{
    Task<DriversLicense?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<DriversLicense?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<DriversLicense?> GetByNumberAsync(string number, CancellationToken cancellationToken = default);

    void Add(DriversLicense user);
}
