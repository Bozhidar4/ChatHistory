using AutoMapper;
using ChatHistory.Api.Dtos;
using ChatHistory.Domain.Events;

namespace ChatHistory.Api.Mappings
{
    public class EventMapping : Profile
    {
        public EventMapping() 
        {
            CreateMap<Event, EventDto>();
        }
    }
}
