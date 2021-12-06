using Floward.OnlineStore.Core.Models;
using Floward.OnlineStore.RabbitMQCore.Providers;
using Floward.OnlineStore.RabbitMQCore.RabbitMQConfigurations;
using Floward.OnlineStore.RabbitMQCore.Services;
using Floward.OnlineStore.RabbitMQCore.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;
using System.Linq;

namespace Floward.OnlineStore.ApplicationCore.DependencyInjection
{
    public static class RabbitMQExtensions
    {
        public static IServiceCollection AddRabbitMQServices(this IServiceCollection services)
        {
            services.AddSingleton(implementationFactory =>
            {
                var rabbitMQConfigurationAppService = implementationFactory.GetRequiredService<RabbitMQConfigurationAppService>();
                var rabbitMQConfig = rabbitMQConfigurationAppService.GetRabbitMQConfigData();

                var rabbitMQFactory = new ConnectionFactory()
                {
                    HostName = rabbitMQConfig.HostName,
                    UserName = rabbitMQConfig.Username,
                    Password = rabbitMQConfig.Password,
                    Port = rabbitMQConfig.Port
                };

                var channel = rabbitMQFactory.CreateConnection().CreateModel();

                channel.ExchangeDeclare(rabbitMQConfig.Exchange, ExchangeType.Direct);
                channel.QueueDeclare(rabbitMQConfig.Queue, true, false, false, null);

                Enum.GetValues(typeof(ProductType)).OfType<ProductType>().ToList()
                    .ForEach(productType => channel.QueueBind(rabbitMQConfig.Queue, rabbitMQConfig.Exchange, $"{productType}"));

                var rabbitMQProvider = new RabbitMQProvider(channel);

                return rabbitMQProvider;
            });

            services.AddSingleton<IRabbitMQAppService, RabbitMQAppService>();

            return services;
        }
    }
}
