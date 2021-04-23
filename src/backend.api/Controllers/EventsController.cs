using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using backend.business.Events.Models;
using backend.business.Events.Queries.GetDetail;
using backend.business.Events.Queries.GetList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NJsonSchema.Annotations;
using NSwag.Annotations;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public EventsController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }
        [HttpGet]
        [OpenApiOperation("GetEventsList", "Returns a list of all events", "Returns all available events to participate in")]
        public Task<IEnumerable<EventListModel>> Get(CancellationToken cancellationToken)
        {
            return _mediatr.Send(new GetEventsListQuery(), cancellationToken);
        }

        [HttpGet("{id}")]
        [OpenApiOperation("GetEventDetail", "Returns a detail of a single event","Returns the event identified by the id path parameter")]
        public Task<EventDetailModel> Get([NotNull] string id, CancellationToken cancellationToken)
        {
            return _mediatr.Send(new GetEventDetailQuery {Id = id}, cancellationToken);
        }
    }
}
