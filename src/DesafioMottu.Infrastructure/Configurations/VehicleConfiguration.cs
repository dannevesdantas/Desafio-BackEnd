using DesafioMottu.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioMottu.Infrastructure.Configurations;

internal sealed class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("vehicles");

        builder.HasKey(vehicle => vehicle.Id);

        builder.Property(vehicle => vehicle.Model)
            .HasMaxLength(200)
            .HasConversion(model => model.Value, value => new Model(value));

        builder.Property(vehicle => vehicle.Year);

        builder.Property(vehicle => vehicle.LicensePlate)
            .HasMaxLength(200)
            .HasConversion(licensePlate => licensePlate.Number, value => new LicensePlate(value));

        builder.Property(vehicle => vehicle.LastRentedOnUtc);

        builder.Property<uint>("Version").IsRowVersion();
    }
}
