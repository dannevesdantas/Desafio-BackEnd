using DesafioMottu.Domain.Abstractions;

namespace DesafioMottu.Domain.Rentals;

public static class PlanErrors
{
    public static readonly Error NotFound = new(
        "Plan.NotFound",
        "Nenhum plano com a quantidade de dias especificada foi encontrado");
}
