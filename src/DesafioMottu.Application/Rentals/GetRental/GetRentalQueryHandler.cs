using System.Data;
using Dapper;
using DesafioMottu.Application.Abstractions.Data;
using DesafioMottu.Application.Abstractions.Messaging;
using DesafioMottu.Domain.Abstractions;
using DesafioMottu.Domain.Rentals;

namespace DesafioMottu.Application.Rentals.GetRental;

internal sealed class GetRentalQueryHandler : IQueryHandler<GetRentalQuery, RentalResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetRentalQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<RentalResponse>> Handle(GetRentalQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                id AS identificador,
                plan_days AS plano,
                plan_daily_price AS valor_diaria,
                total_price_amount AS valor_total,
                user_id AS entregador_id,
                moto_id AS moto_id,
                duration_start AS data_inicio,
                duration_end AS data_termino,
                predicted_end_date AS data_previsao_termino,
                returned_on_utc AS data_devolucao
            FROM rentals
            WHERE id = @RentalId
            """;

        RentalResponse? rental = await connection.QueryFirstOrDefaultAsync<RentalResponse>(
            sql,
            new
            {
                RentalId = request.LocacaoId
            });

        if (rental is null)
        {
            return Result.Failure<RentalResponse>(RentalErrors.NotFound);
        }

        return rental;
    }
}
