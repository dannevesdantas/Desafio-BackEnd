using DesafioMottu.Application.IntegrationTests.Infrastructure;
using DesafioMottu.Application.Motorcycles.SearchVehicles;
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
    public async Task SearchApartments_ShouldReturnEmptyList_WhenLicensePlateInvalid()
    {
        // Arrange
        string invalidLicensePlate = "blablabla";
        var query = new SearchVehicleQuery(invalidLicensePlate);

        // Act
        Result<IReadOnlyList<VehicleResponse>> result = await Sender.Send(query);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEmpty();
    }

    [Fact]
    public async Task SearchApartments_ShouldReturnApartments_WhenDateRangeIsValid()
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
