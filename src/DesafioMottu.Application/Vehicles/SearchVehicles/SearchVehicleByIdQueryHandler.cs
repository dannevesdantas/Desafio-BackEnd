using System.Data;
using Dapper;
using DesafioMottu.Application.Abstractions.Data;
using DesafioMottu.Application.Abstractions.Messaging;
using DesafioMottu.Domain.Abstractions;
using DesafioMottu.Domain.Vehicles;

namespace DesafioMottu.Application.Vehicles.SearchVehicles;

internal sealed class SearchVehicleByIdQueryHandler : IQueryHandler<SearchVehicleByIdQuery, VehicleResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public SearchVehicleByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<VehicleResponse>> Handle(SearchVehicleByIdQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                v.id AS identificador,
                v.year AS ano,
                v.model AS modelo,
                v.license_plate AS placa
            FROM vehicles AS v
            WHERE
                v.id = @Id
            """;

        VehicleResponse? vehicle = await connection
            .QueryFirstOrDefaultAsync<VehicleResponse>(
                sql,
                new
                {
                    Id = request.VehicleId
                });

        if (vehicle is null)
        {
            return Result.Failure<VehicleResponse>(VehicleErrors.NotFound);
        }

        return vehicle;
    }
}
