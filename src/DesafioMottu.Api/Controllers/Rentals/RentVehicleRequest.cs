namespace DesafioMottu.Api.Controllers.Rentals;

public sealed record RentVehicleRequest(
    Guid entregador_id,
    Guid moto_id,
    DateTime data_inicio,
    DateTime data_termino,
    DateTime data_previsao_termino,
    int plano);
