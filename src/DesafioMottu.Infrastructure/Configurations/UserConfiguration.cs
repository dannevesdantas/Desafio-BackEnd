using DesafioMottu.Domain.DriversLicense;
using DesafioMottu.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioMottu.Infrastructure.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(user => user.Id);

        builder.OwnsOne(user => user.Name);

        builder.Property(user => user.Email)
            .HasMaxLength(400)
            .HasConversion(email => email.Value, value => new Domain.Users.Email(value));

        builder.HasIndex(user => user.Email).IsUnique();

        builder.Property(user => user.Cnpj)
            .HasMaxLength(50)
            .HasConversion(cnpj => cnpj.Value, value => new Cnpj(value));

        builder.HasIndex(user => user.Cnpj)
            .IsUnique();

        builder.HasOne(user => user.DriversLicense)
            .WithOne(license => license.User)
            .HasForeignKey<DriversLicense>(license => license.UserId)
            .IsRequired(false);

        builder.Property(user => user.BirthDate);

        builder.Property<uint>("Version").IsRowVersion();
    }
}
