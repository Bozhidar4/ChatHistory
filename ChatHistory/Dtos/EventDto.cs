using ChatHistory.Domain.EventTypes;
using ChatHistory.Domain.Users;

namespace ChatHistory.Api.Dtos
{
    public class EventDto
    {
        public int Id { get; set; }
        public string? Comment { get; set; }
        public DateTime DateTime { get; set; }
        public int EventTypeId { get; set; }
        public EventType? EventType { get; set; }
        public User? User { get; set; }
        public User? ReceiverUser { get; set; }
    }
}
