namespace DesafioMottu.Domain.Rentals;

public interface IRentalRepository
{
    Task<Rental?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> IsOverlappingAsync(
        Vehicles.Vehicle vehicle,
        DateRange duration,
        CancellationToken cancellationToken = default);

    Task<List<Rental>> GetByVehicleIdAsync(Guid vehicleId, CancellationToken cancellationToken = default);

    void Add(Rental rental);
}
