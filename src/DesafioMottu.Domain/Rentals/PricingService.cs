using DesafioMottu.Domain.Shared;

namespace DesafioMottu.Domain.Rentals;

public class PricingService
{
    public PricingDetails CalculatePrice(DateRange period, DateTime predictedEndDate, DateTime returnedOnUtc, Plan plan)
    {
        ArgumentNullException.ThrowIfNull(period);
        ArgumentNullException.ThrowIfNull(predictedEndDate);
        ArgumentNullException.ThrowIfNull(returnedOnUtc);
        ArgumentNullException.ThrowIfNull(plan);

        Currency currency = Currency.Brl;

        decimal totalPrice = 0;

        if (returnedOnUtc.Date == predictedEndDate.Date)
        {
            int daysCount = (returnedOnUtc.Date - period.Start.Date).Days + 1;
            decimal regularPrice = daysCount * plan.DailyPrice;
            totalPrice += regularPrice;
        }

        // Quando a data informada for inferior a data prevista do término
        if (returnedOnUtc.Date < predictedEndDate.Date)
        {
            // será cobrado o valor das diárias e uma multa adicional
            int notEffectiveDaysCount = (predictedEndDate.Date - returnedOnUtc.Date).Days;
            int effectiveDaysCount = (returnedOnUtc.Date - period.Start.Date).Days + 1;
            decimal effectiveDaysPrice = effectiveDaysCount * plan.DailyPrice;
            // Para plano de X dias o valor da multa é de X% sobre o valor das diárias não efetivadas.
            decimal notEffectiveDaysFee = notEffectiveDaysCount * plan.DailyPrice * plan.DailyFeePercentage;
            totalPrice += effectiveDaysPrice + notEffectiveDaysFee;
        }

        // Quando a data informada for superior a data prevista do término
        if (returnedOnUtc.Date > predictedEndDate.Date)
        {
            // será cobrado um valor adicional de R$50,00 por diária adicional.
            int exceededDaysCount = (returnedOnUtc.Date - predictedEndDate.Date).Days;
            decimal exceededDaysFee = plan.AditionalFeePerDay * exceededDaysCount;
            int regularDaysCount = (predictedEndDate.Date - period.Start.Date).Days + 1;
            decimal regularPrice = regularDaysCount * plan.DailyPrice;
            totalPrice += regularPrice + exceededDaysFee;
        }

        var totalPriceMoney = new Money(totalPrice, currency);

        return new PricingDetails(totalPriceMoney);
    }
}
