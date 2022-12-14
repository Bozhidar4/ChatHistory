using ChatHistory.Api.Dtos;
using ChatHistory.Api.Requests;
using ChatHistory.Core;
using ChatHistory.Shared.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatHistory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatHistoryController : ApiControllerBase
    {
        public ChatHistoryController(
            IMediator mediator,
            IUnitOfWork unitOfWork) : base(mediator, unitOfWork)
        { }

        [HttpGet("{date:DateTime}/{aggregationLevel}")]
        public async Task<IActionResult> GetAllByAggregationLevel(
            [FromRoute] DateTime date,
            AggregationLevelEnum aggregationLevel)
        {
            return await Ok(new GetEventRequest(date, aggregationLevel));
        }
    }
}
