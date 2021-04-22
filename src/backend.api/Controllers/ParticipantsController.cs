using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using backend.business.Backers.Commands.Notify;
using backend.business.Backers.Queries.GetList;
using backend.business.Events.Queries.GetDetail;
using backend.business.Participants.Commands.Finish;
using backend.business.Participants.Commands.Register;
using backend.business.Participants.Commands.Start;
using backend.business.Participants.Models;
using backend.business.Participants.Queries.GetDetail;
using backend.business.Participants.Queries.GetList;
using backend.Filters;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NJsonSchema.Annotations;
using NSwag.Annotations;

namespace backend.Controllers
{
    [ApiController]
    [Route("events/{eventId}/participants")]
    public class ParticipantsController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public ParticipantsController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }
        [HttpGet]
        [OpenApiOperation("GetParticipantsList")]
        public Task<IEnumerable<ParticipantListModel>> Get([NotNull] string eventId, CancellationToken cancellationToken)
        {
            return _mediatr.Send(new GetParticipantsListQuery { EventId = eventId }, cancellationToken);
        }
        [HttpPost]
        [OpenApiOperation("RegisterParticipant")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ParticipantDetailModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionModel))]
        public async Task<IActionResult> Post([NotNull] string eventId, [FromBody] RegisterParticipantCommand command, CancellationToken cancellationToken)
        {
            command.EventId = eventId;
            var createdParticipant = await _mediatr.Send(command, cancellationToken);
            return CreatedAtAction(nameof(Get), new { eventId, createdParticipant.Id }, createdParticipant);
        }

        [HttpGet("{id}")]
        [OpenApiOperation("GetParticipantDetail")]
        public Task<ParticipantDetailModel> Get([NotNull] string eventId, string id, CancellationToken cancellationToken)
        {
            return _mediatr.Send(new GetParticipantDetailQuery { EventId = eventId, Id = id }, cancellationToken);
        }

        [HttpPatch("{id}/start")]
        [OpenApiOperation("StartEventForParticipant")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionModel))]
        public async Task<IActionResult> Start([NotNull] string eventId, [NotNull] string id, CancellationToken cancellationToken)
        {
            await _mediatr.Send(new StartParticipantCommand { EventId = eventId, ParticipantId = id }, cancellationToken);
            return NoContent();
        }

        [HttpPatch("{id}/finish")]
        [OpenApiOperation("FinishEventForParticipant", "Updates the status for a given participant for a given event to Finished")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionModel))]
        public async Task<IActionResult> Finish([NotNull] string eventId, [NotNull] string id,[FromBody] FinishParticipantCommand command, CancellationToken cancellationToken)
        {
            command.EventId = eventId;
            command.ParticipantId = id;
            await _mediatr.Send(command, cancellationToken);
            await _mediatr.Send(new NotifyBackersCommand
            {
                Participant = await _mediatr.Send(new GetParticipantDetailQuery { EventId = eventId, Id = id }, cancellationToken),
                Backers = await _mediatr.Send(new GetBackersListQuery { EventId = eventId, ParticipantId = id }, cancellationToken),
                Event = await _mediatr.Send(new GetEventDetailQuery { Id = eventId }, cancellationToken)
            }, cancellationToken); 
            return NoContent();
        }
    }
}