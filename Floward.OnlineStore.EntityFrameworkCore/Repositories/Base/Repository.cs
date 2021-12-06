using EFCore.BulkExtensions;
using Floward.OnlineStore.Core.IRepositories.Base;
using Floward.OnlineStore.Core.Requests.Filters;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Floward.OnlineStore.EntityFrameworkCore.Repositories.Base
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        protected readonly ILogger _logger;

        public Repository(DbContext context, ILogger logger)
        {
            Context = context;
            _logger = logger;
        }

        protected FlowardOnlineStoreDbContext FlowardOnlineStoreDbContext
        {
            get { return Context as FlowardOnlineStoreDbContext; }
        }

        public async Task<TEntity> GetAsync(int id)
        {
            // Here we are working with a DbContext, not PlutoContext. So we don't have DbSets 
            // such as Courses or Authors, and we need to use the generic Set() method to access them.
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            // Note that here I've repeated Context.Set<TEntity>() in every method and this is causing
            // too much noise. I could get a reference to the DbSet returned from this method in the 
            // constructor and store it in a private field like _entities. This way, the implementation
            // of our methods would be cleaner:
            // 
            // _entities.ToList();
            // _entities.Where();
            // _entities.SingleOrDefault();
            // 
            // I didn't change it because I wanted the code to look like the videos. But feel free to change
            // this on your own.
            return Context.Set<TEntity>();
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().AnyAsync(predicate);
        }

        public IEnumerable<TEntity> Pagenate(PaginationFilter filter, out int totalRecords, Expression<Func<TEntity, bool>> predicate)
        {
            var itemQuery = Find(predicate);
            var items = itemQuery.Skip((filter.PageNumber - 1) * filter.PageSize)
                                 .Take(filter.PageSize)
                                 .ToList();
            totalRecords = itemQuery.Count();

            return items;
        }

        public async Task InsertOrUpdateBulkInternalAsync(IEnumerable<TEntity> data, List<string> updateByProp,
            List<string> allPropertiesToExclude = null, int batchSize = 10000)
        {
            var bulkConfig = new BulkConfig
            {
                BatchSize = batchSize,
                UpdateByProperties = updateByProp,
                PropertiesToExclude = allPropertiesToExclude,
                UseTempDB = true
            };

            var strategy = FlowardOnlineStoreDbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await FlowardOnlineStoreDbContext.Database.BeginTransactionAsync();
                await FlowardOnlineStoreDbContext.BulkInsertOrUpdateAsync(data.ToList(), bulkConfig);
                await transaction.CommitAsync();
            });
        }


        public async Task InsertBulkInternalAsync(IEnumerable<TEntity> data, int batchSize = 10000)
        {
            var bulkConfig = new BulkConfig
            {
                BatchSize = batchSize,
                UseTempDB = true,
                WithHoldlock = false
            };

            var strategy = FlowardOnlineStoreDbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await FlowardOnlineStoreDbContext.Database.BeginTransactionAsync();
                await FlowardOnlineStoreDbContext.BulkInsertAsync(data.ToList(), bulkConfig);
                await transaction.CommitAsync();
            });
        }
    }
}
