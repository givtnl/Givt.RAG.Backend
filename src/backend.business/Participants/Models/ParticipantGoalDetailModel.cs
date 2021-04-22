using backend.domain;
using NJsonSchema.Annotations;

namespace backend.business.Participants.Models
{
    public class ParticipantGoalDetailModel
    {
        /// <summary>
        /// Either DonationAmount, Distance or Time
        /// </summary>
        [NotNull]
        public ParticipantGoalType Type { get; set; }
        [NotNull]
        public string Value { get; set; }
    }
}