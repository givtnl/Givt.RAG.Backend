using backend.business.Backers.Models;
using backend.business.Events.Models;
using backend.business.Participants.Models;
using backend.domain;

namespace backend.business.Backers.Messages
{
    public class NotifyBackersMessage
    {
        public BackerListModel Backer { get; set; }
        public ParticipantDetailModel Participant { get; set; }
        public EventDetailModel Event { get; set; }
    }
}