using System.Text;
using System.Text.Json;
using LocadoraDVD.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace LocadoraDVD.Infra.Data.Messaging;
public class MessageBusService : IMessageBusService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IConfiguration _configuration;
    private const string _exchange = "locadora-service";
    public MessageBusService(IConfiguration configuration)
    {
        _configuration = configuration;
        var connectionFactory = new ConnectionFactory
        {
            HostName = Environment.GetEnvironmentVariable("RABITMQHOST") ?? _configuration.GetSection("RabbitMQ").GetSection("Host").Value,
            UserName = Environment.GetEnvironmentVariable("RABITMQUSER") ?? _configuration.GetSection("RabbitMQ").GetSection("User").Value,
            Password = Environment.GetEnvironmentVariable("RABITMQPASSWORD") ?? _configuration.GetSection("RabbitMQ").GetSection("Password").Value,
        };
        _connection = connectionFactory.CreateConnection("locadora-dvd");

        _channel = _connection.CreateModel();
    }

    public void Publish(object data, string routingKey)
    {
        var type = data.GetType();

        var payload = JsonSerializer.Serialize(data);
        var byteArray = Encoding.UTF8.GetBytes(payload);

        _channel.BasicPublish(_exchange, routingKey, null, byteArray);
    }
}