using backend.business.Participants.Models;
using MediatR;
using NJsonSchema.Annotations;
using NSwag.Annotations;

namespace backend.business.Participants.Commands.Register
{
    /// <summary>
    /// Used to register a participant with a certain event
    /// </summary>
    public class RegisterParticipantCommand : IRequest<ParticipantDetailModel>
    {
        [NotNull]
        [OpenApiIgnore]
        public string EventId { get; set; }
        [NotNull]
        public string Name { get; set; }

    }
}