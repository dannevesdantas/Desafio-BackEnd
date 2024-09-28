namespace DesafioMottu.Application.EventBus;

public interface IEventBus
{
    Task PublishAsync(string message, CancellationToken cancellationToken = default);
}
