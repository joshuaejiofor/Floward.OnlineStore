using Floward.OnlineStore.RabbitMQCore.Providers.Interfaces;
using RabbitMQ.Client;

namespace Floward.OnlineStore.RabbitMQCore.Providers
{
    public class RabbitMQProvider : IRabbitMQProvider
    {
        public RabbitMQProvider(IModel rabbitMQChannel)
        {
            RabbitMQChannel = rabbitMQChannel;
        }
        public IModel RabbitMQChannel { get; private set; }
    }
}
