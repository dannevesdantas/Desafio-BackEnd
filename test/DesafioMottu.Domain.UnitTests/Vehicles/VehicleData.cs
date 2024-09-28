using DesafioMottu.Domain.Vehicles;

namespace DesafioMottu.Domain.UnitTests.Vehicles;

internal static class VehicleData
{
    public static Vehicle Create() => Vehicle.Create(
        new Model("Mottu E"),
        2024,
        new LicensePlate("BRA-2024"));
}
