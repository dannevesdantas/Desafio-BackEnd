namespace DesafioMottu.Api.Controllers.Entregadores;

public sealed record RegisterUserRequest(
    string nome,
    string cnpj,
    DateOnly data_nascimento,
    string numero_cnh,
    string tipo_cnh,
    string? imagem_cnh);
