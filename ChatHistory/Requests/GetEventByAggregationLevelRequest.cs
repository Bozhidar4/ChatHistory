using ChatHistory.Api.Dtos;
using MediatR;
using Microsoft.OpenApi.Extensions;

namespace ChatHistory.Api.Requests
{
    public class GetEventByAggregationLevelRequest : IRequest<IEnumerable<string>>
    {
        public AggregationLevelEnum AggregationLevel { get; set; }

        public GetEventByAggregationLevelRequest(AggregationLevelEnum aggregationLevel)
        {
            AggregationLevel= aggregationLevel;
        }
    }
}
