using AutoMapper;
using ChatHistory.Api.Dtos;
using ChatHistory.Api.Services.Interfaces;
using ChatHistory.Domain.Events;
using ChatHistory.Domain.Users;
using MediatR;

namespace ChatHistory.Api.Requests.Handlers
{
    public class GetEventRequestHandler :
        IRequestHandler<GetEventRequest, IEnumerable<string>>
    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;
        private readonly IFormatDataService _formatDataService;

        public GetEventRequestHandler(
            IMapper mapper,
            IEventRepository eventRepository,
            IFormatDataService formatDataService)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _formatDataService = formatDataService;
        }

        public async Task<IEnumerable<string>> Handle(GetEventRequest request, CancellationToken cancellationToken)
        {
            var events = _mapper.Map<IEnumerable<EventDto>>(
                await _eventRepository.GetAllInAscendingOrderAsync());

            return _formatDataService.FormatDataContinuously(events);
        }
    }
}
