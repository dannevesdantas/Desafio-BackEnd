using DesafioMottu.Application.IntegrationTests.Infrastructure;
using DesafioMottu.Application.Rentals.GetRental;
using DesafioMottu.Domain.Abstractions;
using DesafioMottu.Domain.Rentals;
using FluentAssertions;

namespace DesafioMottu.Application.IntegrationTests.Rentals;

public class GetRentalTests : BaseIntegrationTest
{
    private static readonly Guid RentalId = Guid.NewGuid();

    public GetRentalTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task GetRental_ShouldReturnFailure_WhenRentalIsNotFound()
    {
        // Arrange
        var query = new GetRentalQuery(RentalId);

        // Act
        Result<RentalResponse> result = await Sender.Send(query);

        // Assert
        result.Error.Should().Be(RentalErrors.NotFound);
    }
}
