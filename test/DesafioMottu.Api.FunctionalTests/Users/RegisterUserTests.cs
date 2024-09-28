using System.Net;
using System.Net.Http.Json;
using DesafioMottu.Api.Controllers.Entregadores;
using DesafioMottu.Api.FunctionalTests.Infrastructure;
using FluentAssertions;

namespace DesafioMottu.Api.FunctionalTests.Users;

public class RegisterUserTests : BaseFunctionalTest
{
    public RegisterUserTests(FunctionalTestWebAppFactory factory)
        : base(factory)
    {
    }

    public static TheoryData<string, string, DateOnly, string, string> Cases =
        new()
        {
            { "first last", "00.000.000/0000-00", new DateOnly(2500, 1, 1), "00000000000", "X" },
            { "first last", "00.000.000/0000-00", new DateOnly(2100, 09, 15), "00000000000", "Y" },
            { "first last", "00.000.000/0000-00", new DateOnly(2319, 10, 2), "00000000000", "X" },
            { "first last", "00.000.000/0000-00", new DateOnly(2700, 3, 20), "00000000000", "Z" },
            { "last", "00.000.000/0000-00", new DateOnly(2900, 2, 28), "00000000000", "X" },
            { "first", "00.000.000/0000-00", new DateOnly(2150, 6, 12), "00000000000", "Y" },
            { "first last", "00.000.000/0000-00", new DateOnly(2200, 8, 2), "00000000000", "Z" },
            { "first last", "00.000.000/0000-00", new DateOnly(2099, 4, 10), "00000000000", "X" },
            { "first last", "00.000.000/0000-00", new DateOnly(2100, 4, 5), "00000000000", "@" },
            { "first last", "00.000.000/0000-00", new DateOnly(2547, 2, 23), "00000000000", "*" },
            { "first last", "00.000.000/0000-00", new DateOnly(2239, 8, 24), "00000000000", "W" }
        };

    [Theory]
    [MemberData(nameof(Cases))]
    public async Task Register_ShouldReturnBadRequest_WhenRequestIsInvalid(
        string name,
        string cnpj,
        DateOnly birthDate,
        string driversLicenseNumber,
        string driversLicenseTypes)
    {
        // Arrange
        var request = new RegisterUserRequest(name, cnpj, birthDate, driversLicenseNumber, driversLicenseTypes, null);

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("entregadores", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Register_ShouldReturnOk_WhenRequestIsValid()
    {
        // Arrange
        var request = new RegisterUserRequest("first last", "07.934.156/0001-99", new DateOnly(1987, 8, 2), "50185291526", "A", null);

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("entregadores", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}
