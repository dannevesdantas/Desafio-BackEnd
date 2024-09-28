using DesafioMottu.Application.EventBus;
using DesafioMottu.Domain.Abstractions;
using Google.Api.Gax;
using Google.Cloud.PubSub.V1;
using Grpc.Core;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Evently.Common.Infrastructure.EventBus;

internal sealed class EventBus : IEventBus
{
    private readonly IConfiguration _configuration;

    public EventBus(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task PublishAsync<T>(T entity, CancellationToken cancellationToken = default)
        where T : Entity
    {
        string projectId = _configuration["PubSub:ProjectId"];
        string topicId = _configuration["PubSub:TopicId"];

        PublisherServiceApiClient publisherService = await new PublisherServiceApiClientBuilder
        {
            EmulatorDetection = EmulatorDetection.EmulatorOrProduction
        }.BuildAsync();

        TopicName topicName = new TopicName(projectId, topicId);
        CreateTopic(projectId, topicId);

        PublisherClient publisher = await new PublisherClientBuilder
        {
            TopicName = topicName,
            EmulatorDetection = EmulatorDetection.EmulatorOrProduction
        }.BuildAsync();

        await publisher.PublishAsync(JsonConvert.SerializeObject(entity));
        await publisher.ShutdownAsync(TimeSpan.FromSeconds(15));
    }

    private static Topic CreateTopic(string projectId, string topicId)
    {
        PublisherServiceApiClient publisher = PublisherServiceApiClient.Create();
        var topicName = TopicName.FromProjectTopic(projectId, topicId);
        Topic topic = null;

        try
        {
            topic = publisher.CreateTopic(topicName);
            Console.WriteLine($"Topic {topic.Name} created.");
        }
        catch (RpcException e) when (e.Status.StatusCode == StatusCode.AlreadyExists)
        {
            Console.WriteLine($"Topic {topicName} already exists.");
        }
        return topic;
    }
}
