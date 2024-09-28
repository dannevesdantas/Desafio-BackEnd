using DesafioMottu.Domain.DriversLicense;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioMottu.Infrastructure.Configurations;

internal sealed class DriversLicenseConfiguration : IEntityTypeConfiguration<DriversLicense>
{
    public void Configure(EntityTypeBuilder<DriversLicense> builder)
    {
        builder.ToTable("drivers_licenses");

        builder.HasKey(license => license.Id);

        builder.Property(license => license.Number)
            .HasMaxLength(50);

        builder.HasIndex(license => license.Number).IsUnique();

        builder.Property(license => license.Types);

        builder.Property<uint>("Version").IsRowVersion();
    }
}
