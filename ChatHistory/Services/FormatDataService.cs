using ChatHistory.Api.Dtos;
using ChatHistory.Api.Services.Interfaces;
using System.Text;

namespace ChatHistory.Api.Services
{
    public class FormatDataService : IFormatDataService
    {
        public IEnumerable<string> FormatDataContinuously(IEnumerable<EventDto> events)
        {
            var results = new List<string>();
            foreach (var e in events)
            {
                results.Add(GenerateContinuouslyResultMessage(e));
            }

            return results;
        }

        public IEnumerable<string> FormatDataHourly(IEnumerable<EventDto> events)
        {
            var messages = new Dictionary<string, List<string>>();
            var eventMessages = new EventMessages();

            foreach (var e in events)
            {
                GenerateHourlyResultMessage(e, eventMessages, messages);
            }

            var results = new List<string>();

            foreach (var keyValuePair in messages)
            {
                int index = 0;

                foreach (var item in keyValuePair.Value)
                {
                    
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (index == 0)
                        {
                            results.Add($"{keyValuePair.Key}:00: {item}");
                        }
                        else
                        {
                            results.Add($"       {item}");
                        }
                    }

                    index++;
                }
            }

            return results;
        }

        public IEnumerable<string> FormatDataDaily(IEnumerable<EventDto> events)
        {
            var messages = new Dictionary<string, List<string>>();
            var eventMessages = new EventMessages();

            foreach (var e in events)
            {
                GenerateDailyResultMessage(e, eventMessages, messages);
            }

            var results = new List<string>();

            foreach (var keyValuePair in messages)
            {
                int index = 0;

                foreach (var item in keyValuePair.Value)
                {

                    if (!string.IsNullOrEmpty(item))
                    {
                        if (index == 0)
                        {
                            results.Add($"{keyValuePair.Key}: {item}");
                        }
                        else
                        {
                            results.Add($"       {item}");
                        }
                    }

                    index++;
                }
            }

            return results;
        }

        private string GenerateContinuouslyResultMessage(EventDto e)
        {
            var message = new StringBuilder($"{e.DateTime.ToShortTimeString()}: {e.User?.Name}");

            switch (e.EventTypeId)
            {
                case (int)EventTypeEnumDto.Enter:
                    message.Append($" enters the room");
                    break;
                case (int)EventTypeEnumDto.Leave:
                    message.Append($" leaves");
                    break;
                case (int)EventTypeEnumDto.Comment:
                    message.Append($" comments: '{e.Comment}'");
                    break;
                case (int)EventTypeEnumDto.HighFive:
                    message.Append($" high-fives {e.ReceiverUser?.Name}");
                    break;
                default:
                    break;
            }

            return message.ToString();
        }

        private void GenerateHourlyResultMessage(EventDto e,
                                                 EventMessages eventMessages,
                                                 Dictionary<string, List<string>> results)
        {
            switch (e.EventTypeId)
            {
                case (int)EventTypeEnumDto.Enter:
                    eventMessages.EntriesCount++;
                    bool multipleEntries = eventMessages.EntriesCount > 1;
                    eventMessages.EntriesMessage = $"{eventMessages.EntriesCount} {(multipleEntries ? "people" : "person")} entered";
                    break;
                case (int)EventTypeEnumDto.Leave:
                    eventMessages.LeavingsCount++;
                    bool multipleLeavings = eventMessages.LeavingsCount > 1;
                    eventMessages.LeavingsMessage = $"{eventMessages.LeavingsCount} {(multipleLeavings ? "people" : "person")} left";
                    break;
                case (int)EventTypeEnumDto.Comment:
                    eventMessages.CommentsCount++;
                    bool multipleComments = eventMessages.CommentsCount > 1;
                    eventMessages.CommentsMessage = $"{eventMessages.CommentsCount} comment{(multipleComments ? "s" : string.Empty)}";
                    break;
                case (int)EventTypeEnumDto.HighFive:
                    eventMessages.HighFivesCount++;
                    bool multipleHighFives = eventMessages.HighFivesCount > 1;
                    eventMessages.HighFivesMessage = $"{eventMessages.HighFivesCount} {(multipleHighFives ? "people" : "person")} high-fived";
                    break;
                default:
                    break;
            }

            var eventMessagesCollection = new List<string>() {
                eventMessages.EntriesMessage,
                eventMessages.LeavingsMessage,
                eventMessages?.CommentsMessage,
                eventMessages?.HighFivesMessage };

            if (!results.ContainsKey(e.DateTime.Hour.ToString()))
            {
                results.TryAdd(e.DateTime.Hour.ToString(), eventMessagesCollection);
            }
            else
            {
                results.TryGetValue(e.DateTime.Hour.ToString(), out var messages);
                messages?.Clear();
                messages?.AddRange(eventMessagesCollection);
            }
        }

        private void GenerateDailyResultMessage(EventDto e, EventMessages eventMessages, Dictionary<string, List<string>> results)
        {
            switch (e.EventTypeId)
            {
                case (int)EventTypeEnumDto.Enter:
                    eventMessages.EntriesCount++;
                    bool multipleEntries = eventMessages.EntriesCount > 1;
                    eventMessages.EntriesMessage = $"{eventMessages.EntriesCount} {(multipleEntries ? "people" : "person")} entered";
                    break;
                case (int)EventTypeEnumDto.Leave:
                    eventMessages.LeavingsCount++;
                    bool multipleLeavings = eventMessages.LeavingsCount > 1;
                    eventMessages.LeavingsMessage = $"{eventMessages.LeavingsCount} {(multipleLeavings ? "people" : "person")} left";
                    break;
                case (int)EventTypeEnumDto.Comment:
                    eventMessages.CommentsCount++;
                    bool multipleComments = eventMessages.CommentsCount > 1;
                    eventMessages.CommentsMessage = $"{eventMessages.CommentsCount} comment{(multipleComments ? "s" : string.Empty)}";
                    break;
                case (int)EventTypeEnumDto.HighFive:
                    eventMessages.HighFivesCount++;
                    bool multipleHighFives = eventMessages.HighFivesCount > 1;
                    eventMessages.HighFivesMessage = $"{eventMessages.HighFivesCount} {(multipleHighFives ? "people" : "person")} high-fived";
                    break;
                default:
                    break;
            }

            var eventMessagesCollection = new List<string>() {
                eventMessages.EntriesMessage,
                eventMessages.LeavingsMessage,
                eventMessages?.CommentsMessage,
                eventMessages?.HighFivesMessage };

            if (!results.ContainsKey(e.DateTime.Date.ToString("dd/MM")))
            {
                results.TryAdd(e.DateTime.Date.ToString("dd/MM"), eventMessagesCollection);
            }
            else
            {
                results.TryGetValue(e.DateTime.Date.ToString("dd/MM"), out var messages);
                messages?.Clear();
                messages?.AddRange(eventMessagesCollection);
            }
        }
    }
}
