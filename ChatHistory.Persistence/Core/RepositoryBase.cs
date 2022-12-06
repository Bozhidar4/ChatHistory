using ChatHistory.Domain.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ChatHistory.Persistence.Core
{
    public class RepositoryBase<T, C, K> : IRepository<T, K>
        where T : Entity<K>
        where C : DbContext
    {
        protected readonly DbSet<T> _set;
        protected readonly DbContext _dbContext;

        protected DbSet<T> Query
        {
            get { return _set; }
            set { }
        }

        public RepositoryBase(C dbContext)
        {
            _dbContext = dbContext;
            _set = dbContext.Set<T>();
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _set.AsNoTracking().ToListAsync();
        }

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _set.AsNoTracking().Where(predicate).ToListAsync();
        }
    }
}
