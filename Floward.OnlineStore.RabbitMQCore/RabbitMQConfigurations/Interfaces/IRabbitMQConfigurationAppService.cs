using Floward.OnlineStore.RabbitMQCore.Models;

namespace Floward.OnlineStore.RabbitMQCore.RabbitMQConfigurations.Interfaces
{
    public interface IRabbitMQConfigurationAppService
    {
        RabbitMQConfig GetRabbitMQConfigData();
    }
}
