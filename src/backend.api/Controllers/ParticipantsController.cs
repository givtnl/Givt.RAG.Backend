using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using backend.business.Participants.Commands.Register;
using backend.business.Participants.Models;
using backend.business.Participants.Queries.GetDetail;
using backend.business.Participants.Queries.GetList;
using backend.Filters;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [OpenApiOperation("GetParticipantsList", "Returns a list of participants for a given event")]
        public Task<IEnumerable<ParticipantListModel>> Get(string eventId, CancellationToken cancellationToken)
        {
            return _mediatr.Send(new GetParticipantsListQuery { EventId = eventId }, cancellationToken);
        }
        [HttpPost]
        [OpenApiOperation("RegisterParticipant", "Registers a participant for a given event")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ParticipantDetailModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionModel))]
        public async Task<IActionResult> Post(string eventId,[FromBody]RegisterParticipantCommand command, CancellationToken cancellationToken)
        {
            command.EventId = eventId;
            var createdParticipant = await _mediatr.Send(command, cancellationToken);
            return CreatedAtAction(nameof(Get), new {eventId, createdParticipant.Id}, createdParticipant);
        }

        [HttpGet("{id}")]
        [OpenApiOperation("GetParticipantDetail", "Returns the detail of a participant for a given event")]
        public Task<ParticipantDetailModel> Get(string eventId, string id, CancellationToken cancellationToken)
        {
            return _mediatr.Send(new GetParticipantDetailQuery { EventId = eventId, Id = id }, cancellationToken);
        }
    }
}