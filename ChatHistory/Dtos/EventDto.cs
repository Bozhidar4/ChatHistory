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

        public EventDto(
            int id,
            string? comment,
            DateTime dateTime,
            int eventTypeId,
            EventType eventType,
            User user,
            User? receiverUser)
        {
            Id = id;
            Comment = comment;
            DateTime = dateTime;
            EventTypeId = eventTypeId;
            EventType = eventType;
            User = user;
            ReceiverUser = receiverUser;
        }
    }
}
