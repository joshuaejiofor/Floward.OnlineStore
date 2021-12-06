using AutoMapper;
using Floward.OnlineStore.Core.Requests;
using Floward.OnlineStore.Core.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Threading.Tasks;

namespace Floward.OnlineStore.ApplicationCore.Providers
{
    public abstract class ProductProviderBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly ILogger _logger;
        protected readonly IConfiguration _configuration;


        public ProductProviderBase(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
        }

        public Task AddToCartAsync(PurchaseRequest purchaseRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}
