using NJsonSchema.Annotations;

namespace backend.business.Participants.Models
{
    public class ParticipantListModel
    {
        [NotNull]
        public string Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        public string EventId { get; set; }
    }
}