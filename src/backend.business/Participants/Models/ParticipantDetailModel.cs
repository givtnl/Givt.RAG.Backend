using NJsonSchema.Annotations;

namespace backend.business.Participants.Models
{
    public class ParticipantDetailModel
    {
        [NotNull]
        public string Id { get; set; }
        [NotNull]
        public string Name { get; set; }
    }
}