using DesafioMottu.Domain.Abstractions;
using MediatR;

namespace DesafioMottu.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
