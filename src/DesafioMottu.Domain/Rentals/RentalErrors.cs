using DesafioMottu.Domain.Abstractions;

namespace DesafioMottu.Domain.Rentals;

public static class RentalErrors
{
    public static readonly Error NotFound = new(
        "Rental.Found",
        "O aluguel com o identificador especificado não foi encontrado");

    public static readonly Error Overlap = new(
        "Rental.Overlap",
        "O aluguel atual coincide com outro já existente");

    public static readonly Error DiversLicenseNotAllowed = new(
        "Rental.DiversLicenseNotAllowed",
        "A carteira de motorista do usuário não atende aos requisitos legais");
}
