using ChatHistory.Domain.Events;
using ChatHistory.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace ChatHistory.Persistence.Repositories
{
    public class EventRepository :
        RepositoryBase<Event, AppDbContext, int>,
        IEventRepository
    {
        public EventRepository(AppDbContext dbContext)
            : base(dbContext)
        { }

        public async Task<IEnumerable<Event>> GetByDateAsync(DateTime date)
        {
            return await Query
                .Include(e => e.User)
                .Include(e => e.EventType)
                .Include(e => e.ReceiverUser)
                .Where(e => e.DateTime.Date == date.Date)
                .OrderBy(e => e.DateTime)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
