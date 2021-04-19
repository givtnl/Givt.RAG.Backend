using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using backend.business.Backers.Models;
using backend.business.Backers.Queries.GetList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        [OpenApiOperation("GetBackersList", "Returns a list of backers for a given participant")]
        public Task<IEnumerable<BackerListModel>> Get(string eventId, string participantId, CancellationToken cancellationToken)
        {
            return _mediatr.Send(new GetBackersListQuery { EventId = eventId, ParticipantId = participantId }, cancellationToken);
        }

    }
}