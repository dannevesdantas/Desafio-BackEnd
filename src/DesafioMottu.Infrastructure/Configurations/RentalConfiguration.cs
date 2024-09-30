using DesafioMottu.Domain.Rentals;
using DesafioMottu.Domain.Shared;
using DesafioMottu.Domain.Users;
using DesafioMottu.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioMottu.Infrastructure.Configurations;

internal sealed class RentalConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.ToTable("rentals");

        builder.HasKey(rental => rental.Id);

        builder.OwnsOne(rental => rental.Duration);

        builder.Property(rental => rental.PredictedEndDate);

        builder.Property(rental => rental.ReturnedOnUtc);

        builder.OwnsOne(rental => rental.Plan);

        builder.OwnsOne(rental => rental.TotalPrice, priceBuilder =>
        {
            priceBuilder.Property(money => money.Currency)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });

        builder.HasOne<Vehicle>()
            .WithMany()
            .HasForeignKey(rental => rental.VehicleId);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(rental => rental.UserId);

        builder.Property<uint>("Version").IsRowVersion();
    }
}
