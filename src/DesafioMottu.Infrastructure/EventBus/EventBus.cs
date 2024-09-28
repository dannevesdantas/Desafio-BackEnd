using DesafioMottu.Application.EventBus;
using Google.Api.Gax;
using Google.Cloud.PubSub.V1;
using Grpc.Core;
using Microsoft.Extensions.Configuration;

namespace Evently.Common.Infrastructure.EventBus;

internal sealed class EventBus : IEventBus
{
    private readonly IConfiguration _configuration;

    public EventBus(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task PublishAsync(string message, CancellationToken cancellationToken = default)
    {
        string projectId = _configuration["PubSub:ProjectId"];
        string topicId = _configuration["PubSub:TopicId"];

        PublisherServiceApiClient publisherService = await new PublisherServiceApiClientBuilder
        {
            EmulatorDetection = EmulatorDetection.EmulatorOrProduction
        }.BuildAsync();

        TopicName topicName = new TopicName(projectId, topicId);
        Topic topic = null;

        try
        {
            topic = publisherService.CreateTopic(topicName);
            Console.WriteLine($"Topic {topic.Name} created.");
        }
        catch (RpcException e) when (e.Status.StatusCode == StatusCode.AlreadyExists)
        {
            Console.WriteLine($"Topic {topicName} already exists.");
        }

        PublisherClient publisher = await new PublisherClientBuilder
        {
            TopicName = topicName,
            EmulatorDetection = EmulatorDetection.EmulatorOrProduction
        }.BuildAsync();

        await publisher.PublishAsync(message);
        await publisher.ShutdownAsync(TimeSpan.FromSeconds(15));
    }
}
