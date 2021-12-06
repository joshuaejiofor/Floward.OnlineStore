using Floward.OnlineStore.RabbitMQCore.Models;
using Floward.OnlineStore.RabbitMQCore.RabbitMQConfigurations.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Floward.OnlineStore.RabbitMQCore.RabbitMQConfigurations
{
    public class RabbitMQConfigurationAppService : IRabbitMQConfigurationAppService
    {
        protected IConfiguration _configuration;

        public RabbitMQConfigurationAppService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public RabbitMQConfig GetRabbitMQConfigData()
        {
            var result = _configuration.GetSection($"RabbitMQ").Get<RabbitMQConfig>();
            return result;
        }
    }
}
