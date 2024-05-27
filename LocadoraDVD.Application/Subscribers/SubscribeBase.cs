using System.Text;
using System.Text.Json;
using LocadoraDVD.Domain.Interfaces.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace LocadoraDVD.Application.Subscribers;

public abstract class SubscribeBase<T> : BackgroundService where T : class
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _queue;
    private readonly string _routingKeySubscribe;
    private const string TrackingsExchange = "locadora-service";

    protected SubscribeBase(IConfiguration configuration, IServiceProvider serviceProvider, string queue, string routingKeySubscribe)
    {
        _serviceProvider = serviceProvider;
        _queue = queue;
        _routingKeySubscribe = routingKeySubscribe;

        var connectionFactory = new ConnectionFactory
        {
            HostName = Environment.GetEnvironmentVariable("RABITMQHOST") ?? configuration["RabbitMQ:Host"],
            UserName = Environment.GetEnvironmentVariable("RABITMQUSER") ?? configuration["RabbitMQ:User"],
            Password = Environment.GetEnvironmentVariable("RABITMQPASSWORD") ?? configuration["RabbitMQ:Password"],
        };

        _connection = connectionFactory.CreateConnection("locadora-service-consumer");
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(exchange: TrackingsExchange, type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
        _channel.QueueDeclare(queue: _queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
        _channel.QueueBind(queue: _queue, exchange: TrackingsExchange, routingKey: _routingKeySubscribe);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (sender, eventArgs) =>
        {
            var contentArray = eventArgs.Body.ToArray();
            var contentString = Encoding.UTF8.GetString(contentArray);
            var @event = JsonSerializer.Deserialize<T>(contentString);

            using (var scope = _serviceProvider.CreateScope())
            {
                var unitOfWorkMongo = scope.ServiceProvider.GetRequiredService<IUnitOfWorkMongo>();

                await CompleteAsync(unitOfWorkMongo, @event);
            }

            _channel.BasicAck(eventArgs.DeliveryTag, false);
        };

        _channel.BasicConsume(queue: _queue, autoAck: false, consumer: consumer);

        return Task.CompletedTask;
    }

    protected abstract Task CompleteAsync(IUnitOfWorkMongo unitOfWorkMongo, T @event);
}
