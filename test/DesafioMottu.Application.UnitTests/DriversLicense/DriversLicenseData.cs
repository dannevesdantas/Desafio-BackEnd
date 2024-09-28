namespace DesafioMottu.Domain.UnitTests.DriversLicense;

internal static class DriversLicenseData
{
    public static Domain.DriversLicense.DriversLicense Create(Guid UserId) => Domain.DriversLicense.DriversLicense.Create(UserId, Number, Types);

    public static readonly string Number = "68234859262";
    public static readonly List<char> Types = new List<char>() { 'A' };
    public static readonly string? ImageBase64 = null;
}
