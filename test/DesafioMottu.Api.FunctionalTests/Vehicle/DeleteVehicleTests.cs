using System.Net;
using DesafioMottu.Api.FunctionalTests.Infrastructure;
using FluentAssertions;

namespace DesafioMottu.Api.FunctionalTests.Users;

public class DeleteVehicleTests : BaseFunctionalTest
{
    public DeleteVehicleTests(FunctionalTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Delete_ShouldReturnBadRequest_WhenRequestIsInvalid()
    {
        // Arrange
        string id = "foo bar";

        // Act
        HttpResponseMessage response = await HttpClient.DeleteAsync($"motos/{id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
