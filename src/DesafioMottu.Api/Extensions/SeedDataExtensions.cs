using System.Data;
using System.Diagnostics.CodeAnalysis;
using Bogus;
using Dapper;
using DesafioMottu.Application.Abstractions.Data;

namespace DesafioMottu.Api.Extensions;

[ExcludeFromCodeCoverage]
internal static class SeedDataExtensions
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        ISqlConnectionFactory sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        var faker = new Faker();

        List<object> motos = new();
        for (int i = 0; i < 100; i++)
        {
            motos.Add(new
            {
                Id = Guid.NewGuid(),
                Model = faker.PickRandom(new string[] { "Mottu Sport", "Mottu E", "Mottu Pop" }),
                Year = faker.Random.Int(2020, DateTime.Now.Year),
                LicensePlate = faker.Random.Replace("???#?##"),
                LastRentedOnUtc = faker.Date.Past()
            });
        }

        const string sql = """
            INSERT INTO public.vehicles
            (id, model, year, license_plate, last_rented_on_utc)
            VALUES(@Id, @Model, @Year, @LicensePlate, @LastRentedOnUtc);
            """;

        connection.Execute(sql, motos);
    }
}
