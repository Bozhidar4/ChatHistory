using ChatHistory.Domain.Core;

namespace ChatHistory.Domain.EventTypes
{
    public class EventType : Entity<int>
    {
        public string? Name { get; set; }

        public EventType(
            int id,
            string name) : base(id)
        {
            Name = name;
        }
    }
}
