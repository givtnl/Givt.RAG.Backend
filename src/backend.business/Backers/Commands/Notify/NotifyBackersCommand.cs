using System.Collections.Generic;
using backend.business.Backers.Models;
using backend.business.Events.Models;
using backend.business.Participants.Models;
using MediatR;

namespace backend.business.Backers.Commands.Notify
{
    public class NotifyBackersCommand : IRequest
    {
        public EventDetailModel Event { get; set; }
        public IEnumerable<BackerListModel> Backers { get; set; }
        public ParticipantDetailModel Participant { get; set; }
    }
}