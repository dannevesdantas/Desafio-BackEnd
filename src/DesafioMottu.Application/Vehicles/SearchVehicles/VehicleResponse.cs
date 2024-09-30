namespace DesafioMottu.Application.Vehicles.SearchVehicles;

public sealed class VehicleResponse
{
    public Guid identificador { get; init; }

    public int ano { get; init; }

    public string modelo { get; init; }

    public string placa { get; init; }
}
