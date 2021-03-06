using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using backend.business.Backers.Commands.Checkout;
using backend.business.Backers.Commands.Register;
using backend.business.Backers.Models;
using backend.business.Backers.Queries.GetDetail;
using backend.business.Backers.Queries.GetList;
using backend.Filters;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NJsonSchema.Annotations;
using NSwag.Annotations;

namespace backend.Controllers
{
    [ApiController]
    [Route("events/{eventId}/participants/{participantId}/backers")]
    public class BackersController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public BackersController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }
        [HttpGet]
        [OpenApiOperation( "GetBackersList", "Returns a list of backers", "Returns the backers for a given parcipant for a given event")]
        public Task<IEnumerable<BackerListModel>> Get([NotNull]string eventId, [NotNull] string participantId, CancellationToken cancellationToken)
        {
            return _mediatr.Send(new GetBackersListQuery { EventId = eventId, ParticipantId = participantId }, cancellationToken);
        }
        [HttpGet("{id}")]
        [OpenApiOperation("GetBackerDetail", "Returns a detail of a single backer", "Returns the backer identified by the id path parameter")]
        public Task<BackerDetailModel> Get([NotNull] string eventId, [NotNull] string participantId, [NotNull] string id, CancellationToken cancellationToken)
        {
            return _mediatr.Send(new GetBackerDetailQuery { ParticipantId = participantId, EventId = eventId, Id = id }, cancellationToken);
        }
        [HttpPost]
        [OpenApiOperation("RegisterBacker", "Registers a new backer for a given participant", "Registers a new backer who is backing the giving participant who participates in an given event")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BackerDetailModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionModel))]
        public async Task<IActionResult> Post([NotNull] string eventId, [NotNull] string participantId, [FromBody] RegisterBackerCommand command, CancellationToken cancellationToken)
        {
            command.EventId = eventId;
            command.ParticipantId = participantId;
            var createdBacker = await _mediatr.Send(command, cancellationToken);
            return CreatedAtAction(nameof(Get), new { eventId, participantId, createdBacker.Id }, createdBacker);
        }
    }
}