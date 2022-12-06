using AutoMapper;
using ChatHistory.Api.Dtos;
using ChatHistory.Api.Services.Interfaces;
using ChatHistory.Domain.Events;
using MediatR;

namespace ChatHistory.Api.Requests.Handlers
{
    public class GetEventByAggregationLevelRequestHandler :
        IRequestHandler<GetEventByAggregationLevelRequest, IEnumerable<string>>
    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;
        private readonly IFormatDataService _formatDataService;

        public GetEventByAggregationLevelRequestHandler(
            IMapper mapper,
            IEventRepository eventRepository,
            IFormatDataService formatDataService)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _formatDataService = formatDataService;
        }

        public async Task<IEnumerable<string>> Handle(GetEventByAggregationLevelRequest request, CancellationToken cancellationToken)
        {
            var events = _mapper.Map<IEnumerable<EventDto>>(
                await _eventRepository.GetAllInAscendingOrderAsync());

            switch (request.AggregationLevel)
            {
                case AggregationLevelEnum.Continuously:
                    return _formatDataService.FormatDataContinuously(events);
                case AggregationLevelEnum.Hourly:
                    return _formatDataService.FormatDataHourly(events);
                case AggregationLevelEnum.Daily:
                    return _formatDataService.FormatDataDaily(events);
                default:
                    return new List<string>();
            }
        }
    }
}
