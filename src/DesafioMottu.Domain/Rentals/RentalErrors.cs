using DesafioMottu.Domain.Abstractions;

namespace DesafioMottu.Domain.Rentals;

public static class RentalErrors
{
    public static readonly Error NotFound = new(
        "Rental.Found",
        "O aluguel com o identificador especificado não foi encontrado");

    public static readonly Error Overlap = new(
        "Rental.Overlap",
        "O veículo em questão já foi alugado nesse período");

    public static readonly Error DiversLicenseNotAllowed = new(
        "Rental.DiversLicenseNotAllowed",
        "A carteira de motorista do usuário não atende aos requisitos legais");

    public static readonly Error TooEarly = new(
        "Rental.TooEarly",
        "O veículo não pode ser devolvido antes da data de início do aluguel");

    public static readonly Error AlreadyReturned = new(
        "Rental.AlreadyReturned",
        "O veículo já foi devolvido");

    public static readonly Error InvalidEndDate = new(
        "Rental.InvalidEndDate",
        "A data final não é válida para o plano escolhido");
}
