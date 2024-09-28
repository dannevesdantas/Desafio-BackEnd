namespace DesafioMottu.Application.Rentals.GetRental;

public sealed class RentalResponse
{
    public Guid identificador { get; init; }

    public int plano { get; init; }

    public decimal valor_diaria { get; init; }

    public decimal valor_total { get; init; }

    public Guid entregador_id { get; init; }

    public Guid moto_id { get; init; }

    public DateTime data_inicio { get; init; }

    public DateTime data_termino { get; init; }

    public DateTime data_previsao_termino { get; init; }

    public DateTime? data_devolucao { get; init; }
}
