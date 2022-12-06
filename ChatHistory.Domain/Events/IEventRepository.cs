using ChatHistory.Domain.Core;

namespace ChatHistory.Domain.Events
{
    public interface IEventRepository : IRepository<Event, int>
    {
        Task<IEnumerable<Event>> GetAllInAscendingOrderAsync();
    }
}
