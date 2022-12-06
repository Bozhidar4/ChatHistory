using MediatR;

namespace ChatHistory.Api.Requests
{
    public class GetEventRequest : IRequest<IEnumerable<string>>
    {
    }
}
