using Floward.OnlineStore.ApplicationCore.Providers.Interfaces;
using Floward.OnlineStore.Core.Models;

namespace Floward.OnlineStore.ApplicationCore.Factories.Interfaces
{
    public interface IProductProviderFactory
    {
        IProductProvider Create(ProductType productType);
    }
}
