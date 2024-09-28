using Asp.Versioning;
using DesafioMottu.Application.Motorcycles.DeleteVehicle;
using DesafioMottu.Application.Motorcycles.RegisterVehicle;
using DesafioMottu.Application.Motorcycles.SearchVehicles;
using DesafioMottu.Application.Motorcycles.UpdateVehicle;
using DesafioMottu.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DesafioMottu.Api.Controllers.Motos;

[ApiController]
[ApiVersionNeutral]
[Route("motos")]
public class MotosController : ControllerBase
{
    private readonly ISender _sender;

    public MotosController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Cadastrar uma nova moto")]
    public async Task<IActionResult> RegisterVehicle(
        RegisterVehicleRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RegisterVehicleCommand(request.ano, request.modelo, request.placa);

        Result<Guid> result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(nameof(SearchVehicleById), new { id = result.Value }, result.Value);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Consultar motos existentes")]
    public async Task<IActionResult> SearchVehicles([FromQuery] string? placa, CancellationToken cancellationToken)
    {
        var query = new SearchVehicleQuery(placa);

        Result<IReadOnlyList<VehicleResponse>> result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }

    [HttpPut("{id}/placa")]
    [SwaggerOperation(Summary = "Modificar a placa de uma moto")]
    public async Task<IActionResult> UpdateLicensePlateNumber(
        [FromRoute] Guid id,
        UpdateVehicleRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateVehicleCommand(id, request.placa);

        Result<Guid> result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Consultar motos existentes por id")]
    public async Task<IActionResult> SearchVehicleById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new SearchVehicleByIdQuery(id);

        Result<VehicleResponse> result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Remover uma moto")]
    public async Task<IActionResult> DeleteVehicle([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteVehicleCommand(id);

        Result<Guid> result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }
}
