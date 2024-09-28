using DesafioMottu.Domain.Abstractions;

namespace DesafioMottu.Application.EventBus;

public interface IEventBus
{
    Task PublishAsync<T>(T entity, CancellationToken cancellationToken = default)
        where T : Entity;
}
