using ChatHistory.Shared.Persistence;

namespace ChatHistory.Persistence
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool IsDisposed { get; private set; }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            _dbContext.Dispose();

            GC.SuppressFinalize(this);

            IsDisposed = true;
        }
    }
}
