namespace DesafioMottu.Api.Controllers.Vehicles;

public sealed record RegisterVehicleRequest(
    int ano,
    string modelo,
    string placa);
