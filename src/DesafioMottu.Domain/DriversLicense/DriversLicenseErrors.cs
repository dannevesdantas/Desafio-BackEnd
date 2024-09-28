using DesafioMottu.Domain.Abstractions;

namespace DesafioMottu.Domain.Users;

public static class DriversLicenseErrors
{
    public static readonly Error NotFound = new(
        "DriversLicense.NotFound",
        "The driver's license specified identifier was not found");

    public static readonly Error AlreadyExists = new(
        "DriversLicense.AlreadyExists",
        "The driver's license already exists");

    public static readonly Error InvalidLicenseType = new(
        "DriversLicense.InvalidLicenseType",
        "The provided driver's license type is invalid");
}
