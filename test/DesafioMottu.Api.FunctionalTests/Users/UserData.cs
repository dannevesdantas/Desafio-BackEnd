using DesafioMottu.Api.Controllers.Users;

namespace DesafioMottu.Api.FunctionalTests.Users;

internal static class UserData
{
    public static RegisterUserRequest RegisterTestUserRequest = new(Name, Cnpj, BirthDate, DriversLicenseNumber, DriversLicenseClasses, DriversLicenseImageBase64);

    public static readonly string Name = "Fulano Silva";
    public static readonly string Cnpj = "69608899000126";
    public static readonly DateOnly BirthDate = new(1990, 1, 1);
    public static readonly string DriversLicenseNumber = "95060043005";
    public static readonly string DriversLicenseClasses = "A";
    public static readonly string? DriversLicenseImageBase64 = null;
}
