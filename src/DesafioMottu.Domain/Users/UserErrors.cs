using DesafioMottu.Domain.Abstractions;

namespace DesafioMottu.Domain.Users;

public static class UserErrors
{
    public static readonly Error NotFound = new(
        "User.NotFound",
        "O usuário com o identificador especificado não foi encontrado");

    public static readonly Error AlreadyExists = new(
        "User.AlreadyExists",
        "O usuário já existe");

    public static readonly Error Unlicensed = new(
        "User.Unlicensed",
        "O usuário não possui carteira de motorista");
}
