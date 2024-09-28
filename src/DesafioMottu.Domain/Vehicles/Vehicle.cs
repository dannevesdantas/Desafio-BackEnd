using DesafioMottu.Domain.Abstractions;
using DesafioMottu.Domain.Vehicles.Events;

namespace DesafioMottu.Domain.Vehicles;

public sealed class Vehicle : Entity
{
    private Vehicle(
        Guid id,
        Model model,
        int year,
        LicensePlate licensePlate)
        : base(id)
    {
        Model = model;
        Year = year;
        LicensePlate = licensePlate;
    }

    private Vehicle()
    {
    }

    public Model Model { get; private set; }

    public int Year { get; private set; }

    public LicensePlate LicensePlate { get; private set; }

    public DateTime? LastRentedOnUtc { get; internal set; }

    public static Vehicle Create(Model model, int year, LicensePlate licensePlate)
    {
        var moto = new Vehicle(Guid.NewGuid(), model, year, licensePlate);

        moto.RaiseDomainEvent(new VehicleRegisteredDomainEvent(moto.Id));

        return moto;
    }

    public void SetLicensePlateNumber(string number)
    {
        LicensePlate = new LicensePlate(number);
    }
}
