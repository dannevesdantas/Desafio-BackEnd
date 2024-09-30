namespace DesafioMottu.Api.Controllers.Users;

public sealed record RegisterUserRequest(
    string nome,
    string cnpj,
    DateOnly data_nascimento,
    string numero_cnh,
    string tipo_cnh,
    string? imagem_cnh);
