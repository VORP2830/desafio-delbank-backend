namespace LocadoraDVD.Domain.Interfaces;

public interface IMessageBusService
{
    void Publish(object data, string routingKey);
}
