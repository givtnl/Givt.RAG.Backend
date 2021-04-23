using System.Threading;
using System.Threading.Tasks;
using backend.business.Backers.Commands.Checkout;
using backend.business.Backers.Models;
using backend.Filters;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NJsonSchema.Annotations;
using NSwag.Annotations;

namespace backend.Controllers
{
    [ApiController]
    [Route("events/{eventId}/participants/{participantId}/backers/{backerId}")]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public PaymentsController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }
        [HttpPost]
        [OpenApiOperation("Pay", "Creates an paymentrequest with the paymentprovider", "Creates an paymentrequest with the paymentprovider and returns the URL to the user to finish the payment")]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(BackerPaymentRequestModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionModel))]
        public async Task<IActionResult> Pay([NotNull] string eventId, [NotNull] string participantId,[NotNull]string backerId, [FromBody] CheckoutBackerCommand command, CancellationToken cancellationToken)
        {
            command.EventId = eventId;
            command.BackerId = backerId;
            command.ParticipantId = participantId;
            var paymentRequest = await _mediatr.Send(command, cancellationToken);
            return StatusCode(StatusCodes.Status202Accepted, paymentRequest);
        }
    }
}