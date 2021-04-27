using System.Threading;
using System.Threading.Tasks;
using backend.business.AttachmentsController.Models;
using backend.business.AttachmentsController.Queries.GetDetail;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NJsonSchema.Annotations;
using NSwag.Annotations;

namespace backend.Controllers
{
    [ApiController]
    [Route("events/{eventId}/participants/{participantId}/attachments")]
    public class AttachmentsController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public AttachmentsController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        [OpenApiOperation("GetAttachmentDetail", "Returns a detail of an single attachment for a participant", "Returns the attachment for a single participant who participated in a single event")]
        public Task<AttachmentDetailModel> Get([NotNull] string eventId, [NotNull] string participantId, CancellationToken cancellationToken)
        {
            return _mediatr.Send(new GetAttachmentsDetailQuery { ParticipantId = participantId, EventId = eventId }, cancellationToken);
        }
    }
}