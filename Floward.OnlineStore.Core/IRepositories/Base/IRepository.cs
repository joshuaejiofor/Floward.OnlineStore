using Floward.OnlineStore.Core.Requests.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Floward.OnlineStore.Core.IRepositories.Base
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetAsync(int id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Pagenate(PaginationFilter filter, out int totalRecords, Expression<Func<TEntity, bool>> predicate);
        Task InsertOrUpdateBulkInternalAsync(IEnumerable<TEntity> data, List<string> updateByProp, List<string> allPropertiesToExclude = null, int batchSize = 10000);
        Task InsertBulkInternalAsync(IEnumerable<TEntity> data, int batchSize = 10000);
    }
}
