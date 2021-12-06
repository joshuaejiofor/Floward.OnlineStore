using Floward.OnlineStore.ApplicationCore.Providers.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Floward.OnlineStore.ApplicationCore.DependencyInjection
{
    public static class ProviderCollectionExtensions
    {
        public static IServiceCollection AddProviders(this IServiceCollection services, bool useSingleton = false)
        {
            var iProviders = new List<Type> { typeof(IProductProvider) };
            var applicationAssembly = Assembly.Load("Floward.OnlineStore.ApplicationCore");
            applicationAssembly.GetTypes()
                               .Where(item => item.GetInterfaces().Any(type => iProviders.Contains(type) || type.IsGenericType) && !item.IsInterface)
                               .ToList().ForEach(assignedTypes =>
                               {
                                   if (useSingleton)
                                       services.AddSingleton(assignedTypes);
                                   else
                                       services.AddScoped(assignedTypes);
                               });

            return services;
        }
    }
}
