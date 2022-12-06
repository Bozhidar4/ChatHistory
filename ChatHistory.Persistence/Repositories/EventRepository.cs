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

        public async Task<IEnumerable<Event>> GetAllInAscendingOrderAsync()
        {
            return GetEvents();
        }

        

        public async Task<IEnumerable<Event>> GetAllByGranularityLevelAsync()
        {
            var events = await Query
                .Include(e => e.User)
                .Include(e => e.EventType)
                .Include(e => e.ReceiverUser)
                .OrderBy(e => e.DateTime)
                .ToListAsync();

            return events;
        }

        private IEnumerable<Event> GetEvents()
        {
            return Query
                .Include(e => e.User)
                .Include(e => e.EventType)
                .Include(e => e.ReceiverUser)
                .OrderBy(e => e.DateTime);
        }
    }
}
