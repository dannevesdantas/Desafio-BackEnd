using DesafioMottu.Domain.Rentals;
using DesafioMottu.Domain.Shared;
using DesafioMottu.Domain.Users;
using DesafioMottu.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioMottu.Infrastructure.Configurations;

internal sealed class LocacaoConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.ToTable("rentals");

        builder.HasKey(locacao => locacao.Id);

        builder.OwnsOne(locacao => locacao.Duration);

        builder.Property(locacao => locacao.PredictedEndDate);

        builder.Property(locacao => locacao.ReturnedOnUtc);

        builder.OwnsOne(locacao => locacao.Plan);

        builder.OwnsOne(locacao => locacao.TotalPrice, priceBuilder =>
        {
            priceBuilder.Property(money => money.Currency)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });

        builder.HasOne<Vehicle>()
            .WithMany()
            .HasForeignKey(locacao => locacao.MotoId);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(locacao => locacao.UserId);

        builder.Property<uint>("Version").IsRowVersion();
    }
}
