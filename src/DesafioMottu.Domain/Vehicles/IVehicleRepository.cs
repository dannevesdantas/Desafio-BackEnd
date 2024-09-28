﻿namespace DesafioMottu.Domain.Vehicles;

public interface IVehicleRepository
{
    Task<Vehicle?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Vehicle?> GetByLicensePlateAsync(LicensePlate licensePlate, CancellationToken cancellationToken = default);

    void Add(Vehicle moto);

    void Delete(Vehicle moto);
}
