using backend.business.Participants.Models;
using MediatR;

namespace backend.business.Participants.Queries.GetDetail
{
    /// <summary>
    /// Gets the detail about a certain participant
    /// </summary>
    public class GetParticipantDetailQuery : IRequest<ParticipantDetailModel>
    {
        public string EventId { get; set; }
        public string Id { get; set; }
    }
}