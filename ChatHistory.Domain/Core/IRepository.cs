using System.Linq.Expressions;

namespace ChatHistory.Domain.Core
{
    public interface IRepository<TEntity, K> where TEntity : Entity<K>
    {
    }
}
