using DesafioMottu.Domain.Abstractions;
using DesafioMottu.Domain.Users;
using DesafioMottu.Domain.Users.Events;

namespace DesafioMottu.Domain.DriversLicense;

public sealed class DriversLicense : Entity
{
    private DriversLicense(Guid id, Guid userId, string number, List<char> types)
        : base(id)
    {
        Id = id;
        UserId = userId;
        Number = number;
        Types = types;
    }

    private DriversLicense()
    {
    }

    public Guid UserId { get; private set; }

    public User User { get; private set; }

    public string Number { get; private set; }

    public List<char> Types { get; private set; }

    public static DriversLicense Create(Guid userId, string number, List<char> types)
    {
        var driversLicense = new DriversLicense(Guid.NewGuid(), userId, number, types);

        driversLicense.RaiseDomainEvent(new DriversLicenseCreatedDomainEvent(driversLicense.Id));

        return driversLicense;
    }
}
