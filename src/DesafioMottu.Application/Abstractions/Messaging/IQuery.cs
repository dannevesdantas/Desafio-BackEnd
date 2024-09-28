using DesafioMottu.Domain.Abstractions;
using MediatR;

namespace DesafioMottu.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
