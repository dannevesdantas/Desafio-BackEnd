using DesafioMottu.Domain.Abstractions;

namespace DesafioMottu.Domain.Users;

public static class DriversLicenseErrors
{
    public static readonly Error NotFound = new(
        "DriversLicense.NotFound",
        "A carteira de motorista não foi encontrado");

    public static readonly Error AlreadyExists = new(
        "DriversLicense.AlreadyExists",
        "Essa carteira de motorista já existe");

    public static readonly Error InvalidLicenseClass = new(
        "DriversLicense.InvalidLicenseClass",
        "O(s) tipo(s) de habilitação fornecidos são inválidos");
}
