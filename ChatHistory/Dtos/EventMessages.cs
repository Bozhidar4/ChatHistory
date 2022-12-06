namespace ChatHistory.Api.Dtos
{
    public class EventMessages
    {
        public int EntriesCount { get; set; }
        public int LeavingsCount { get; set; }
        public int CommentsCount { get; set; }
        public int HighFivesCount { get; set; }
        public string? EntriesMessage { get; set; }
        public string? LeavingsMessage { get; set; }
        public string? CommentsMessage { get; set; }
        public string? HighFivesMessage { get; set; }
    }
}
