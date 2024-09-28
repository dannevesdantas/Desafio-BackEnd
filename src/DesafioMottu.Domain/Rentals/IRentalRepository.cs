namespace DesafioMottu.Domain.Rentals;

public interface IRentalRepository
{
    Task<Rental?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> IsOverlappingAsync(
        Vehicles.Vehicle moto,
        DateRange duration,
        CancellationToken cancellationToken = default);

    Task<List<Rental>> GetByMotoIdAsync(Guid motoId, CancellationToken cancellationToken = default);

    void Add(Rental rental);
}
