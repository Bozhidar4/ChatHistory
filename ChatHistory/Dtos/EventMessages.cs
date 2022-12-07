using System.Text;

namespace ChatHistory.Api.Dtos
{
    public class EventMessages
    {
        public int EntriesCount { get; set; }
        public int LeavingsCount { get; set; }
        public int CommentsCount { get; set; }
        public int HighFivesCount { get; set; }
        public StringBuilder? EntriesMessage { get; set; } = new StringBuilder();
        public StringBuilder? LeavingsMessage { get; set; } = new StringBuilder();
        public StringBuilder? CommentsMessage { get; set; } = new StringBuilder();
        public StringBuilder? HighFivesMessage { get; set; } = new StringBuilder();
    }
}
