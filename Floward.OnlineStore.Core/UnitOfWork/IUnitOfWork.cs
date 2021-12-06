using Floward.OnlineStore.Core.IRepositories;
using System;
using System.Threading.Tasks;

namespace Floward.OnlineStore.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IProductRepository ProductRepository { get; }
        public IOrderRepository OrderRepository { get; }

        Task ExecuteSqlAsync(string statement);
        Task<int> CompleteAsync();
    }
}
