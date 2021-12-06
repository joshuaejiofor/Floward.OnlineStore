using AutoMapper;
using Floward.OnlineStore.ApplicationCore.Factories.Interfaces;
using Floward.OnlineStore.ApplicationCore.Services.Interfaces;
using Floward.OnlineStore.Core.Models;
using Floward.OnlineStore.Core.Requests;
using Floward.OnlineStore.Core.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Threading.Tasks;

namespace Floward.OnlineStore.ApplicationCore.Services
{
    public class ProductService : ServiceBase, IProductService
    {
        private readonly IProductProviderFactory _productProviderFactory;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger, IConfiguration configuration,
            IProductProviderFactory productProviderFactory)
            : base(unitOfWork, mapper, logger, configuration)
        {
            _productProviderFactory = productProviderFactory;
        }

        public async Task ProcessAsync(PurchaseRequest purchaseRequest)
        {
            switch (purchaseRequest.ProductType)
            {
                case ProductType.Flower:
                    await _productProviderFactory.Create(purchaseRequest.ProductType).AddToCartAsync(purchaseRequest);
                    break;
                case ProductType.Food:
                case ProductType.Electronic:
                    await _productProviderFactory.Create(purchaseRequest.ProductType).CalculateDiscountAsync(purchaseRequest);
                    break;                
                default:
                    break;
            }
        }


    }
}