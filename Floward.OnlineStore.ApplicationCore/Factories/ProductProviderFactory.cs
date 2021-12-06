using Floward.OnlineStore.ApplicationCore.Factories.Interfaces;
using Floward.OnlineStore.ApplicationCore.Providers;
using Floward.OnlineStore.ApplicationCore.Providers.Interfaces;
using Floward.OnlineStore.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Floward.OnlineStore.ApplicationCore.Factories
{
    public class ProductProviderFactory : IProductProviderFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ProductProviderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IProductProvider Create(ProductType productType)
        {
            switch (productType)
            {
                case ProductType.Electronic:
                    return _serviceProvider.GetRequiredService<ElectronicProductProvider>();
                case ProductType.Flower:
                    return _serviceProvider.GetRequiredService<FlowerProductProvider>();
                case ProductType.Food:
                    return _serviceProvider.GetRequiredService<FoodProductProvider>();

                default:
                    break;
            }
            return null;
        }
    }
}
