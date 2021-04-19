using MediatR;
using NJsonSchema.Annotations;

namespace backend.business.Participants.Commands.Start
{
    /// <summary>
    /// Used for the participant to start his deelname for the event
    /// </summary>
    public class StartParticipantCommand : IRequest
    {
        [NotNull]
        public string EventId { get; set; }
        [NotNull]
        public string ParticipantId { get; set; }
    }
}