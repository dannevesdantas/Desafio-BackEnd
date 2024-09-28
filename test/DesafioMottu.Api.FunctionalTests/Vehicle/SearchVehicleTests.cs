using System.Net;
using DesafioMottu.Api.FunctionalTests.Infrastructure;
using FluentAssertions;

namespace DesafioMottu.Api.FunctionalTests.Users;

public class SearchVehicleTests : BaseFunctionalTest
{
    public SearchVehicleTests(FunctionalTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Search_ShouldReturnOk_WhenRequestIsValid()
    {
        // Act
        HttpResponseMessage response = await HttpClient.GetAsync($"motos");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory]
    [InlineData("BRA1X23")]
    [InlineData("foo bar")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("Lorem ipsum dolor sit amet, consectetur adipiscing elit.")]
    public async Task Search_ByLicensePlateNum_ShouldReturnOk_WhenRequestIsValid_WithParams(string licensePlateNumber)
    {
        // Act
        HttpResponseMessage response = await HttpClient.GetAsync($"motos?placa={licensePlateNumber}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    [InlineData(-999)]
    public async Task Search_ById_ShouldReturnBadRequest_WhenRequestIsInvalid(int? id)
    {
        // Act
        HttpResponseMessage response = await HttpClient.GetAsync($"motos/{id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
