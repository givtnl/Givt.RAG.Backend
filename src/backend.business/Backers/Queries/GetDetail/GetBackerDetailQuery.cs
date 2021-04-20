using backend.business.Backers.Models;
using MediatR;

namespace backend.business.Backers.Queries.GetDetail
{
    public class GetBackerDetailQuery : IRequest<BackerDetailModel>
    {
        public string ParticipantId { get; set; }
        public string EventId { get; set; }
        public string Id { get; set; }
    }
}