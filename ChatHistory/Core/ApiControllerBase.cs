using ChatHistory.Shared.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatHistory.Core
{
    public class ApiControllerBase : Controller
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public ApiControllerBase(
            IMediator mediator,
            IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        protected async Task<IActionResult> Ok<TResponse>(IRequest<TResponse> query)
        {
            var response = await _mediator.Send(query);
            return base.Ok(response);
        }

        protected async Task<IActionResult> NoContent<TResponse>(IRequest<TResponse> command)
        {
            await _mediator.Send(command);
            await _unitOfWork.SaveChangesAsync();
            return base.NoContent();
        }
    }
}
