using DesafioMottu.Domain.Rentals;
using DesafioMottu.Domain.Shared;
using FluentAssertions;

namespace DesafioMottu.Domain.UnitTests.Rentals;

public class PricingServiceTests
{
    public static TheoryData<int, DateTime, DateTime, DateTime, decimal> Cases =
        new()
        {
            // Plan: 7 days
            { 7, new DateTime(2024, 1, 1), new DateTime(2024, 1, 7), new DateTime(2024, 1, 7), 210.00M }, // Returned on schedule
            { 7, new DateTime(2024, 1, 1), new DateTime(2024, 1, 7), new DateTime(2024, 1, 10), 360.00M }, // Returned late
            { 7, new DateTime(2024, 1, 1), new DateTime(2024, 1, 7), new DateTime(2024, 1, 5), 162.00M }, // Returned in advance

            // Plan: 15 days
            { 15, new DateTime(2024, 1, 1), new DateTime(2024, 1, 15), new DateTime(2024, 1, 15), 420.00M }, // Returned on schedule
            { 15, new DateTime(2024, 1, 1), new DateTime(2024, 1, 15), new DateTime(2024, 1, 17), 520.00M }, // Returned late
            { 15, new DateTime(2024, 1, 1), new DateTime(2024, 1, 15), new DateTime(2024, 1, 13), 386.40M }, // Returned in advance

            // Plan: 30 days
            { 30, new DateTime(2024, 1, 1), new DateTime(2024, 1, 30), new DateTime(2024, 1, 30), 660.00M }, // Returned on schedule
            { 30, new DateTime(2024, 1, 1), new DateTime(2024, 1, 30), new DateTime(2024, 2, 1), 760.00M }, // Returned late
            { 30, new DateTime(2024, 1, 1), new DateTime(2024, 1, 30), new DateTime(2024, 1, 28), 633.60M }, // Returned in advance

            // Plan: 45 days
            { 45, new DateTime(2024, 1, 1), new DateTime(2024, 2, 15), new DateTime(2024, 2, 15), 920.00M }, // Returned on schedule
            { 45, new DateTime(2024, 1, 1), new DateTime(2024, 2, 15), new DateTime(2024, 2, 17), 1020.00M }, // Returned late
            { 45, new DateTime(2024, 1, 1), new DateTime(2024, 2, 15), new DateTime(2024, 2, 13), 896.00M }, // Returned in advance

            // Plan: 50 days
            { 50, new DateTime(2024, 1, 1), new DateTime(2024, 2, 20), new DateTime(2024, 2, 20), 918.00M }, // Returned on schedule
            { 50, new DateTime(2024, 1, 1), new DateTime(2024, 2, 20), new DateTime(2024, 2, 22), 1018.00M }, // Returned late
            { 50, new DateTime(2024, 1, 1), new DateTime(2024, 2, 20), new DateTime(2024, 2, 18), 896.40M }, // Returned in advance
        };

    [Theory]
    [MemberData(nameof(Cases))]
    public void CalculatePrice_Should_ReturnCorrectTotalPrice(
        int days,
        DateTime startDate,
        DateTime endDate,
        DateTime returnedOnUtc,
        decimal totalPrice)
    {
        // Arrange
        var currency = Currency.Brl;
        var period = DateRange.Create(startDate, endDate);
        var predictedEndDate = period.End;
        Plan plan = Plan.Create(days).Value;
        var expectedTotalPrice = new Money(totalPrice, currency);
        var pricingService = new PricingService();

        // Act
        PricingDetails pricingDetails = pricingService.CalculatePrice(period, predictedEndDate, returnedOnUtc, plan);

        // Assert
        pricingDetails.TotalPrice.Should().Be(expectedTotalPrice);
    }
}
