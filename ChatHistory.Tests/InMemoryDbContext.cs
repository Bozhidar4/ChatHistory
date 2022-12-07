using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;

namespace ChatHistory.Tests
{
    public class InMemoryDbContext<T> where T : DbContext
    {
        public InMemoryDbContext(string dbName)
        {
            DbContextOptionsBuilder = new DbContextOptionsBuilder<T>()
                .UseInMemoryDatabase(dbName, new InMemoryDatabaseRoot())
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            InMemoryDatabaseName = dbName;
        }
        public DbContextOptionsBuilder<T> DbContextOptionsBuilder { get; }
        public string InMemoryDatabaseName { get; }
        public T GetDbContext() => (T)Activator.CreateInstance(typeof(T), DbContextOptionsBuilder.Options);

        public void AddEntities<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            using (var context = GetDbContext())
            {
                context.Set<TEntity>().AddRange(entities);
                context.SaveChanges();
            }
        }
        public void AddEntities<TEntity>(TEntity entity) where TEntity : class
        {
            using (var context = GetDbContext())
            {
                context.Set<TEntity>().Add(entity);
                context.SaveChanges();
            }
        }
    }
}
