using backend.domain;
using NJsonSchema.Annotations;

namespace backend.business.Participants.Commands.Register
{
    public class RegisterParticipantGoalCommand
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