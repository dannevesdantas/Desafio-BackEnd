using System.Net;
using System.Net.Http.Json;
using DesafioMottu.Api.Controllers.Vehicles;
using DesafioMottu.Api.FunctionalTests.Infrastructure;
using FluentAssertions;

namespace DesafioMottu.Api.FunctionalTests.Users;

public class RegisterVehicleTests : BaseFunctionalTest
{
    public RegisterVehicleTests(FunctionalTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Theory]
    [InlineData(0, "Fusca", "BRA1S34")]
    [InlineData(1870, null, "ZZZ1S34")]
    [InlineData(1500, "Boeing 747", null)]
    [InlineData(1870, "", "AR")]
    [InlineData(-10, "Cybertruck", null)]
    [InlineData(1234, "", "ABC0X00")]
    public async Task Register_ShouldReturnBadRequest_WhenRequestIsInvalid(int year, string model, string licensePlateNumber)
    {
        // Arrange
        var request = new RegisterVehicleRequest(year, model, licensePlateNumber);

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("motos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Register_ShouldReturnOk_WhenRequestIsValid()
    {
        // Arrange
        var request = new RegisterVehicleRequest(2004, "Honda CB 600F Hornet", "BED1X11");

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("motos", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Register_ShouldReturnBadRequest_WhenLicensePlateAlreadyExists()
    {
        // Arrange
        var request = new RegisterVehicleRequest(2022, "FLUO ABS", "SKQ4X81");

        // Act
        HttpResponseMessage firstResponse = await HttpClient.PostAsJsonAsync("motos", request);
        HttpResponseMessage secondResponse = await HttpClient.PostAsJsonAsync("motos", request);

        // Assert
        firstResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        secondResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
