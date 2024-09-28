using DesafioMottu.Domain.Abstractions;

namespace DesafioMottu.Domain.Rentals;

public sealed record Plan
{
    private Plan()
    {
    }

    public int Days { get; init; }

    public decimal DailyPrice { get; init; }

    public decimal DailyFeePercentage { get; init; }

    public decimal AditionalFeePerDay { get; init; } = 50.00M;

    public static Result<Plan> Create(int days)
    {
        if (days <= 0)
        {
            throw new ArgumentException("Plan must have more than 0 days");
        }

        Plan plan;

        switch (days)
        {
            case 7:
                plan = new Plan { Days = 7, DailyPrice = 30.00M, DailyFeePercentage = 0.20M };
                break;
            case 15:
                plan = new Plan { Days = 15, DailyPrice = 28.00M, DailyFeePercentage = 0.40M };
                break;
            case 30:
                plan = new Plan { Days = 30, DailyPrice = 22.00M, DailyFeePercentage = 0.40M };
                break;
            case 45:
                plan = new Plan { Days = 45, DailyPrice = 20.00M, DailyFeePercentage = 0.40M };
                break;
            case 50:
                plan = new Plan { Days = 50, DailyPrice = 18.00M, DailyFeePercentage = 0.40M };
                break;
            default:
                return Result.Failure<Plan>(PlanErrors.NotFound);
                break;
        }

        return Result.Success(plan);
    }
}
