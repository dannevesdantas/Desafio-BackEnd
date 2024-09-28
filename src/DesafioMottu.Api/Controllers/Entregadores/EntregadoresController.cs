using Asp.Versioning;
using Aspose.Drawing;
using DesafioMottu.Application.Users.RegisterUser;
using DesafioMottu.Application.Users.UpdateUser;
using DesafioMottu.Domain.Abstractions;
using DesafioMottu.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DesafioMottu.Api.Controllers.Entregadores;

[ApiController]
[ApiVersionNeutral]
[Route("entregadores")]
public class EntregadoresController : ControllerBase
{
    private readonly ISender _sender;

    public EntregadoresController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Cadastrar entregador")]
    public async Task<IActionResult> Register(
        RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        Result<List<char>> licenseTypesResult = ParseDriversLicenseTypes(request.tipo_cnh);

        if (licenseTypesResult.IsFailure)
        {
            return BadRequest(licenseTypesResult.Error);
        }

        Image? imagemCnh = null;

        if (!string.IsNullOrEmpty(request.imagem_cnh))
        {
            imagemCnh = LoadImage(request.imagem_cnh);
        }

        var command = new RegisterUserCommand(
            request.nome,
            request.cnpj,
            request.data_nascimento,
            request.numero_cnh,
            licenseTypesResult.Value,
            imagemCnh);

        Result<Guid> result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return StatusCode(StatusCodes.Status201Created, result.Value);
    }

    [HttpPost("{id}/cnh")]
    [SwaggerOperation(Summary = "Enviar foto da CNH")]
    public async Task<IActionResult> SubmitDriversLicense(
        [FromRoute] Guid id,
        UpdateUserRequest request,
        CancellationToken cancellationToken)
    {
        Image imagemCnh = null;

        if (!string.IsNullOrEmpty(request.imagem_cnh))
        {
            imagemCnh = LoadImage(request.imagem_cnh);
        }

        var command = new UpdateUserCommand(id, imagemCnh);

        Result<Guid> result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    private static Result<List<char>> ParseDriversLicenseTypes(string tipoCnh)
    {
        const char Separator = '+';
        var licenseTypes = new List<char>();
        string[] tokens = tipoCnh.Trim().Split(Separator).ToArray();
        foreach (string token in tokens)
        {
            if (token.Length == 1)
            {
                licenseTypes.Add(Convert.ToChar(token));
            }
            else
            {
                return Result.Failure<List<char>>(DriversLicenseErrors.InvalidLicenseType);
            }
        }
        return Result.Success(licenseTypes);
    }

    private static Image LoadImage(string base64String)
    {
        byte[] bytes = Convert.FromBase64String(base64String);

        Image image;
        using (MemoryStream ms = new MemoryStream(bytes))
        {
            image = Image.FromStream(ms);
        }

        return image;
    }
}
