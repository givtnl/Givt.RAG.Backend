using MediatR;
using NJsonSchema.Annotations;

namespace backend.business.Participants.Commands.Finish
{
    /// <summary>
    /// Used to mark the participant as finished and let the followers know he's done!
    /// Start collecting!
    /// </summary>
    public class FinishParticipantCommand : IRequest
    {
        [NotNull]
        [JsonSchemaIgnore]
        public string EventId { get; set; }
        [NotNull]
        [JsonSchemaIgnore]
        public string ParticipantId { get; set; }
        [NotNull]
        public decimal DistanceInMeters { get; set; }
    }
}