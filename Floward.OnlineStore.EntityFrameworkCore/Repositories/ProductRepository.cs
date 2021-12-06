using Floward.OnlineStore.Core.IRepositories;
using Floward.OnlineStore.Core.Models;
using Floward.OnlineStore.EntityFrameworkCore.Repositories.Base;
using Serilog;

namespace Floward.OnlineStore.EntityFrameworkCore.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(FlowardOnlineStoreDbContext flowardOnlineStoreDbContext, ILogger logger)
            : base(flowardOnlineStoreDbContext, logger)
        {
        }
    }
}
