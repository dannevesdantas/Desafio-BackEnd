using DesafioMottu.Domain.Abstractions;

namespace DesafioMottu.Domain.Rentals;

public static class PlanErrors
{
    public static readonly Error NotFound = new(
        "Plan.NotFound",
        "A plan with the specified number of days was not found");
}
