using ChatHistory.Api.Dtos;
using MediatR;

namespace ChatHistory.Api.Requests
{
    public class GetEventRequest : IRequest<IEnumerable<string>>
    {
        public DateTime Date { get; set; }
        public AggregationLevelEnum AggregationLevel { get; set; }

        public GetEventRequest(DateTime date, AggregationLevelEnum aggregationLevel)
        {
            Date = date;
            AggregationLevel = aggregationLevel;
        }
    }
}
