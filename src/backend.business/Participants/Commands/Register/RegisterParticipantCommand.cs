using System.Collections.Generic;
using backend.business.Participants.Models;
using backend.domain;
using MediatR;
using NJsonSchema.Annotations;

namespace backend.business.Participants.Commands.Register
{
    /// <summary>
    /// Used to register a participant with a certain event
    /// </summary>
    public class RegisterParticipantCommand : IRequest<ParticipantDetailModel>
    {
        public RegisterParticipantCommand()
        {
            Goals = new List<RegisterParticipantGoalCommand>();
        }
        [NotNull]
        [JsonSchemaIgnore]
        public string EventId { get; set; }
        [NotNull]
        public string Name { get; set; }
        public List<RegisterParticipantGoalCommand> Goals { get; set; }
    }
}