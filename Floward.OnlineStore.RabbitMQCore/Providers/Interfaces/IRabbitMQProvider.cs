using RabbitMQ.Client;

namespace Floward.OnlineStore.RabbitMQCore.Providers.Interfaces
{
    public interface IRabbitMQProvider
    {
        IModel RabbitMQChannel { get; }
    }
}
