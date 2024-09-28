namespace DesafioMottu.Api.Controllers.Locacao;

public sealed record ReserveRentalRequest(
    Guid entregador_id,
    Guid moto_id,
    DateTime data_inicio,
    DateTime data_termino,
    DateTime data_previsao_termino,
    int plano);
