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
        [OpenApiOperation("GetParticipantsList", "Returns a list of participants", "Returns the participants for a given event")]
        public Task<IEnumerable<ParticipantListModel>> Get([NotNull] string eventId, CancellationToken cancellationToken)
        {
            return _mediatr.Send(new GetParticipantsListQuery { EventId = eventId }, cancellationToken);
        }
        [HttpPost]
        [OpenApiOperation("RegisterParticipant", "Registers a new participant for a given event", "Registers a new participant who participates in the given event")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ParticipantDetailModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionModel))]
        public async Task<IActionResult> Post([NotNull] string eventId, [FromBody] RegisterParticipantCommand command, CancellationToken cancellationToken)
        {
            command.EventId = eventId;
            var createdParticipant = await _mediatr.Send(command, cancellationToken);
            return CreatedAtAction(nameof(Get), new { eventId, createdParticipant.Id }, createdParticipant);
        }

        [HttpGet("{id}")]
        [OpenApiOperation("GetParticipantDetail", "Returns a detail of a single participant", "Returns the participant identified by the id path parameter")]
        public Task<ParticipantDetailModel> Get([NotNull] string eventId, string id, CancellationToken cancellationToken)
        {
            return _mediatr.Send(new GetParticipantDetailQuery { EventId = eventId, Id = id }, cancellationToken);
        }

        [HttpPatch("{id}/start")]
        [OpenApiOperation("StartEventForParticipant", "Marks a given participation in an event as Started", "Marks the given participant for the given event as Started so backers can be notified of the new status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionModel))]
        public async Task<IActionResult> Start([NotNull] string eventId, [NotNull] string id, CancellationToken cancellationToken)
        {
            await _mediatr.Send(new StartParticipantCommand { EventId = eventId, ParticipantId = id }, cancellationToken);
            return NoContent();
        }

        [HttpPatch("{id}/finish")]
        [OpenApiOperation("FinishEventForParticipant", "Marks a given participation in an event as Finished", "Marks the given participant for the given event as Finished so backers can be notified of the new status")]
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