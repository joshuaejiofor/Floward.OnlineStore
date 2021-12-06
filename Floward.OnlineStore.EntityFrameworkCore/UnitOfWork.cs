using Floward.OnlineStore.Core.IRepositories;
using Floward.OnlineStore.Core.UnitOfWork;
using Floward.OnlineStore.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Floward.OnlineStore.EntityFrameworkCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FlowardOnlineStoreDbContext _context;
        private readonly ILogger _logger;
        public UnitOfWork(FlowardOnlineStoreDbContext aceMdSignalDbContext, ILogger logger)
        {
            _context = aceMdSignalDbContext;
            _logger = logger;

            ProductRepository = new ProductRepository(_context, logger);
            OrderRepository = new OrderRepository(_context, logger);
        }

        public IProductRepository ProductRepository { get; private set; }
        public IOrderRepository OrderRepository { get; private set; }

        [DisplayName("{0}")]
        public async Task ExecuteSqlAsync(string statement)
        {
            try
            {
                _logger.Warning("ExecuteSqlAsync() started");
                var result = await _context.Database.ExecuteSqlRawAsync(statement);
                _logger.Warning("ExecuteSqlAsync() ended");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"ExecuteSqlAsync , ErrorMessage : {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }
            }
        }

    }
}
