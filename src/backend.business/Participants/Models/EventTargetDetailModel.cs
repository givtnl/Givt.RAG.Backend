using backend.domain;
using NJsonSchema.Annotations;

namespace backend.business.Participants.Models
{
    public class EventTargetDetailModel
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