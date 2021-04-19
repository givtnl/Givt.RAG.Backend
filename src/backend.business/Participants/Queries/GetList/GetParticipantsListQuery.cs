using System.Collections.Generic;
using backend.business.Participants.Models;
using MediatR;

namespace backend.business.Participants.Queries.GetList
{
    public class GetParticipantsListQuery : IRequest<IEnumerable<ParticipantListModel>>
    {
        public string EventId { get; set; }
    }
}