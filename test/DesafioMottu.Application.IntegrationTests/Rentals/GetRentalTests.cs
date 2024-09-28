using DesafioMottu.Application.IntegrationTests.Infrastructure;
using DesafioMottu.Application.Rentals.GetRental;
using DesafioMottu.Domain.Abstractions;
using DesafioMottu.Domain.Rentals;
using FluentAssertions;

namespace DesafioMottu.Application.IntegrationTests.Rentals;

public class GetRentalTests : BaseIntegrationTest
{
    private static readonly Guid BookingId = Guid.NewGuid();

    public GetRentalTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task GetBooking_ShouldReturnFailure_WhenBookingIsNotFound()
    {
        // Arrange
        var query = new GetRentalQuery(BookingId);

        // Act
        Result<RentalResponse> result = await Sender.Send(query);

        // Assert
        result.Error.Should().Be(RentalErrors.NotFound);
    }
}
