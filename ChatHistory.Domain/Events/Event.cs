using ChatHistory.Domain.Core;
using ChatHistory.Domain.EventTypes;
using ChatHistory.Domain.Users;

namespace ChatHistory.Domain.Events
{
    public class Event : Entity<int>
    {
        public string? Comment { get; set; }
        public DateTime DateTime { get; set; }

        public int EventTypeId { get; set; }
        public EventType? EventType { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public int? ReceiverUserId { get; set; }
        public User? ReceiverUser { get; set; }

        public Event(
            int id,
            string? comment,
            DateTime dateTime,
            int eventTypeId,
            int userId,
            int? receiverUserId) : base(id)
        {
            Comment = comment;
            EventTypeId = eventTypeId;
            UserId = userId;
            ReceiverUserId = receiverUserId;
            DateTime = dateTime;
        }
    }
}
