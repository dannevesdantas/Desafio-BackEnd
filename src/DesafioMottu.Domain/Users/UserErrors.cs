using DesafioMottu.Domain.Abstractions;

namespace DesafioMottu.Domain.Users;

public static class UserErrors
{
    public static readonly Error NotFound = new(
        "User.NotFound",
        "The user with the specified identifier was not found");

    public static readonly Error InvalidCredentials = new(
        "User.InvalidCredentials",
        "The provided credentials were invalid");

    public static readonly Error AlreadyExists = new(
        "User.AlreadyExists",
        "The user already exists");

    public static readonly Error Unlicensed = new(
        "User.Unlicensed",
        "The user doesn't have a driver's license");
}
