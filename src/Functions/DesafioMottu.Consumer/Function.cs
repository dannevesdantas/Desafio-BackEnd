using System;
using System.Threading;
using System.Threading.Tasks;
using CloudNative.CloudEvents;
using Google.Cloud.Functions.Framework;
using Google.Cloud.Functions.Hosting;
using Google.Events.Protobuf.Cloud.PubSub.V1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DesafioMottu.Consumer;

[FunctionsStartup(typeof(Startup))]
public class Function : ICloudEventFunction<MessagePublishedData>
{
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;

    public Function(
        ILogger<Function> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public Task HandleAsync(CloudEvent cloudEvent, MessagePublishedData data, CancellationToken cancellationToken)
    {
        string message = data.Message?.TextData;
        _logger.LogInformation(message);

        try
        {
            ProcessIncomingMessage(data);
            _logger.LogInformation($"Message with message ID {data.Message.MessageId} processed successfully");
        }
        catch (Exception e)
        {
            _logger.LogError($"Failed to process incoming message with message ID {data.Message.MessageId}");
            throw;
        }

        return Task.CompletedTask;
    }

    private void ProcessIncomingMessage(MessagePublishedData data)
    {
        string message = data.Message?.TextData;
        Vehicle vehicleData = Newtonsoft.Json.JsonConvert.DeserializeObject<Vehicle>(message);

        // Assim que a mensagem for recebida, deverá ser armazenada no banco de dados para consulta futura.
        SaveVehicle(vehicleData);

        // Criar um consumidor para notificar quando o ano da moto for "2024"
        CheckVehicleYear(vehicleData);
    }

    private void CheckVehicleYear(Vehicle vehicle)
    {
        if (vehicle.Year == 2024)
        {
            _logger.LogInformation($"The vehicle's year is {vehicle.Year}!");
        }
    }

    private void SaveVehicle(Vehicle vehicle)
    {
        var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
        if (connectionString == null)
        {
            Console.WriteLine("You must set your 'MONGODB_URI' environment variable. To learn how to set it, see https://www.mongodb.com/docs/drivers/csharp/current/quick-start/#set-your-connection-string");
            Environment.Exit(0);
        }

        string databaseName = _configuration["DbConnection:DatabaseName"];
        string collectionName = _configuration["DbConnection:CollectionName"];

        var client = new MongoClient(connectionString);
        var collection = client.GetDatabase(databaseName).GetCollection<BsonDocument>(collectionName);

        BsonDocument newDoc = new BsonDocument {
            { "_id", vehicle.Id.ToString() },
            { "model", vehicle.Model },
            { "year", vehicle.Year },
            { "licensePlateNumber", vehicle.LicensePlateNumber },
        };

        collection.ReplaceOne(
            filter: new BsonDocument("_id", vehicle.Id.ToString()),
            options: new ReplaceOptions { IsUpsert = true },
            replacement: newDoc);
    }
}
