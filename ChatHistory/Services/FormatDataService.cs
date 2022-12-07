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
            var eventMessagesModel = new EventMessages();
            int eventHour = 0;

            foreach (var e in events)
            {
                if (e.DateTime.Hour != eventHour)
                {
                    eventHour = e.DateTime.Hour;
                    eventMessagesModel?.EntriesMessage?.Clear();
                    eventMessagesModel?.LeavingsMessage?.Clear();
                    eventMessagesModel?.CommentsMessage?.Clear();
                    eventMessagesModel?.HighFivesMessage?.Clear();
                }
                GenerateHourlyResultMessage(e, eventMessagesModel, messages);
            }

            var results = new List<string>();

            foreach (var keyValuePair in messages)
            {
                int index = 0;
                var time = $"{keyValuePair.Key}:00:";
                var indent = new string(' ', time.Length);

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
                            results.Add($"{indent} {item}");
                        }

                        index++;
                    }
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
            }

            return message.ToString();
        }

        private void GenerateHourlyResultMessage(EventDto e,
                                                 EventMessages eventMessagesModel,
                                                 Dictionary<string, List<string>> results)
        {
            GenerateMessages(e, eventMessagesModel);
            PopulateMessageCollection(e, eventMessagesModel, results);
        }

        private void GenerateMessages(EventDto e, EventMessages eventMessagesModel)
        {
            switch (e.EventTypeId)
            {
                case (int)EventTypeEnumDto.Enter:
                    eventMessagesModel.EntriesCount++;
                    bool multipleEntries = eventMessagesModel.EntriesCount > 1;

                    eventMessagesModel?.EntriesMessage?.Clear();
                    eventMessagesModel?.EntriesMessage?
                        .Append($"{eventMessagesModel.EntriesCount} {(multipleEntries ? "people" : "person")} entered");
                    break;
                case (int)EventTypeEnumDto.Leave:
                    eventMessagesModel.LeavingsCount++;
                    bool multipleLeavings = eventMessagesModel.LeavingsCount > 1;

                    eventMessagesModel?.LeavingsMessage?.Clear();
                    eventMessagesModel?.LeavingsMessage?
                        .Append($"{eventMessagesModel.LeavingsCount} {(multipleLeavings ? "people" : "person")} left");
                    break;
                case (int)EventTypeEnumDto.Comment:
                    eventMessagesModel.CommentsCount++;
                    bool multipleComments = eventMessagesModel.CommentsCount > 1;

                    eventMessagesModel?.CommentsMessage?.Clear();
                    eventMessagesModel?.CommentsMessage?
                        .Append($"{eventMessagesModel.CommentsCount} comment{(multipleComments ? "s" : string.Empty)}");
                    break;
                case (int)EventTypeEnumDto.HighFive:
                    eventMessagesModel.HighFivesCount++;
                    bool multipleHighFives = eventMessagesModel.HighFivesCount > 1;

                    eventMessagesModel?.HighFivesMessage?.Clear();
                    eventMessagesModel?.HighFivesMessage?
                        .Append($"{eventMessagesModel.HighFivesCount} {(multipleHighFives ? "people" : "person")} high-fived");
                    break;
            }
        }

        private static void PopulateMessageCollection(EventDto e, EventMessages eventMessagesModel, Dictionary<string, List<string>> results)
        {
            var key = e.DateTime.Hour.ToString();

            var eventMessagesCollection = new List<string>() {
                eventMessagesModel?.EntriesMessage?.ToString(),
                eventMessagesModel?.LeavingsMessage?.ToString(),
                eventMessagesModel?.CommentsMessage?.ToString(),
                eventMessagesModel?.HighFivesMessage?.ToString() };

            if (!results.ContainsKey(key))
            {
                results.TryAdd(key, eventMessagesCollection);
            }
            else
            {
                results.TryGetValue(key, out var messages);
                messages?.Clear();
                messages?.AddRange(eventMessagesCollection);
            }
        }
    }
}
