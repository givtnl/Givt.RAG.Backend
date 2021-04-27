using backend.business.AttachmentsController.Models;
using MediatR;

namespace backend.business.AttachmentsController.Queries.GetDetail
{
    public class GetAttachmentsDetailQuery : IRequest<AttachmentDetailModel>
    {
        public string ParticipantId { get; set; }
        public string EventId { get; set; }
    }
}