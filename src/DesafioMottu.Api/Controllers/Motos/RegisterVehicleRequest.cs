namespace DesafioMottu.Api.Controllers.Motos;

public sealed record RegisterVehicleRequest(
    int ano,
    string modelo,
    string placa);
