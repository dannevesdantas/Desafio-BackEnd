﻿using Asp.Versioning;
using DesafioMottu.Application.Rentals.GetRental;
using DesafioMottu.Application.Rentals.ReserveRental;
using DesafioMottu.Application.Rentals.ReturnRental;
using DesafioMottu.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DesafioMottu.Api.Controllers.Locacao;

[ApiController]
[ApiVersionNeutral]
[Route("locacao")]
public class LocacaoController : ControllerBase
{
    private readonly ISender _sender;

    public LocacaoController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Alugar uma moto")]
    public async Task<IActionResult> ReserveLocacao(
        ReserveRentalRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ReserveRentalCommand(
            request.entregador_id,
            request.moto_id,
            request.data_inicio,
            request.data_termino,
            request.data_previsao_termino,
            request.plano);

        Result<Guid> result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(nameof(GetLocacao), new { id = result.Value }, result.Value);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Consultar locação por id")]
    public async Task<IActionResult> GetLocacao([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetRentalQuery(id);

        Result<RentalResponse> result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpPut("{id}/devolucao")]
    [SwaggerOperation(Summary = "Informar data de devolução e calcular valor")]
    public async Task<IActionResult> SubmitDriversLicense(
        [FromRoute] Guid id,
        ReturnRentalRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ReturnRentalCommand(id, request.data_devolucao);

        Result<Guid> result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}
