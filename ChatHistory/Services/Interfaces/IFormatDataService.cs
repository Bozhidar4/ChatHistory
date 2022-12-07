using ChatHistory.Api.Dtos;

namespace ChatHistory.Api.Services.Interfaces
{
    public interface IFormatDataService
    {
        IEnumerable<string> FormatDataContinuously(IEnumerable<EventDto> events);
        IEnumerable<string> FormatDataHourly(IEnumerable<EventDto> events);
    }
}
