using DesafioMottu.Application.IntegrationTests.Infrastructure;
using DesafioMottu.Application.Vehicles.SearchVehicles;
using DesafioMottu.Domain.Abstractions;
using FluentAssertions;

namespace DesafioMottu.Application.IntegrationTests.Vehicles;

public class SearchVehiclesTests : BaseIntegrationTest
{
    public SearchVehiclesTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task SearchVehicles_ShouldReturnEmptyList_WhenLicensePlateNumberInvalid()
    {
        // Arrange
        string invalidLicensePlate = "this is not a valid license plate number";
        var query = new SearchVehicleQuery(invalidLicensePlate);

        // Act
        Result<IReadOnlyList<VehicleResponse>> result = await Sender.Send(query);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEmpty();
    }

    [Fact]
    public async Task SearchVehicles_ShouldReturnVehicles_WhenLicensePlateNumberIsValid()
    {
        // Arrange
        var query = new SearchVehicleQuery(null);

        // Act
        Result<IReadOnlyList<VehicleResponse>> result = await Sender.Send(query);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
    }
}
