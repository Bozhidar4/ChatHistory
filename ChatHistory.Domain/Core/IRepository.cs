using System.Linq.Expressions;

namespace ChatHistory.Domain.Core
{
    public interface IRepository<TEntity, K> where TEntity : Entity<K>
    {
        Task<IList<TEntity>> GetAllAsync();
        Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
