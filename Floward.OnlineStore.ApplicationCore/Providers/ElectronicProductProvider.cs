using AutoMapper;
using Floward.OnlineStore.ApplicationCore.Providers.Interfaces;
using Floward.OnlineStore.Core.Requests;
using Floward.OnlineStore.Core.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Threading.Tasks;

namespace Floward.OnlineStore.ApplicationCore.Providers
{
    public class ElectronicProductProvider : ProductProviderBase, IProductProvider
    {
        public ElectronicProductProvider(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger, IConfiguration configuration)
            : base(unitOfWork, mapper, logger, configuration)
        {
        }

        public async Task CalculateDiscountAsync(PurchaseRequest purchaseRequest)
        {
            await AddToCartAsync(purchaseRequest);
        }
    }
}
