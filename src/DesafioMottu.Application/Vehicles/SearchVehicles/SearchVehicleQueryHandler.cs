using System.Data;
using Dapper;
using DesafioMottu.Application.Abstractions.Data;
using DesafioMottu.Application.Abstractions.Messaging;
using DesafioMottu.Domain.Abstractions;

namespace DesafioMottu.Application.Vehicles.SearchVehicles;

internal sealed class SearchVehicleQueryHandler
    : IQueryHandler<SearchVehicleQuery, IReadOnlyList<VehicleResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public SearchVehicleQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<VehicleResponse>>> Handle(SearchVehicleQuery request, CancellationToken cancellationToken)
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
                @LicensePlateNumber is NULL OR v.license_plate = @LicensePlateNumber
            """;

        IEnumerable<VehicleResponse> vehicles = await connection
            .QueryAsync<VehicleResponse>(
                sql,
                new
                {
                    LicensePlateNumber = request.LicensePlateNumber
                });

        return vehicles.ToList();
    }
}
