using DesafioMottu.Api.Controllers.Entregadores;

namespace DesafioMottu.Api.FunctionalTests.Users;

internal static class UserData
{
    public static RegisterUserRequest RegisterTestUserRequest = new(Nome, Cnpj, BirthDate, NumeroCnh, TiposCnh, ImagemCnh);

    public static readonly string Nome = "First Last";
    public static readonly string Cnpj = "14.143.385/0001-50";
    public static readonly DateOnly BirthDate = new(1990, 1, 1);
    public static readonly string NumeroCnh = "68234859262";
    public static readonly string TiposCnh = "A";
    public static readonly string? ImagemCnh = null;
}
