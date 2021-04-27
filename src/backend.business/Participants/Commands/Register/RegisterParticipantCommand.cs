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
            Targets = new List<RegisterEventTargetCommand>();
        }
        [NotNull]
        [JsonSchemaIgnore]
        public string EventId { get; set; }
        [NotNull]
        public string Name { get; set; }
        public string EntryNumber { get; set; }
        public List<RegisterEventTargetCommand> Targets { get; set; }
    }
}