using backend.domain;
using NJsonSchema.Annotations;

namespace backend.business.Participants.Commands.Register
{
    public class RegisterEventTargetCommand
    {
        /// <summary>
        /// Either DonationAmount, Distance or Time
        /// </summary>
        [NotNull]
        public EventTargetType Type { get; set; }
        [NotNull]
        public string Value { get; set; }
    }
}