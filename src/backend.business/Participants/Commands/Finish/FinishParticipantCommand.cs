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
        public string EventId { get; set; }
        [NotNull]
        public string ParticipantId { get; set; }
    }
}