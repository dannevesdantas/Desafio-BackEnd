using DesafioMottu.Domain.Abstractions;
using DesafioMottu.Domain.Users.Events;

namespace DesafioMottu.Domain.Users;

public sealed class User : Entity
{
    private User(Guid id, Name name, Cnpj cnpj, DateOnly birthDate)
        : base(id)
    {
        Name = name;
        Cnpj = cnpj;
        BirthDate = birthDate;
    }

    private User()
    {
    }

    public Name Name { get; private set; }

    public Email? Email { get; private set; }

    public Cnpj Cnpj { get; private set; }

    public DateOnly BirthDate { get; private set; }

    public int? DriversLicenseId { get; set; } // Optional foreign key property

    public DriversLicense.DriversLicense? DriversLicense { get; private set; }

    public void SetDriversLicense(DriversLicense.DriversLicense driversLicense)
    {
        DriversLicense = driversLicense;
    }

    public static User Create(Name name, Cnpj cnpj, DateOnly birthDate)
    {
        var user = new User(Guid.NewGuid(), name, cnpj, birthDate);

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

        return user;
    }
}
