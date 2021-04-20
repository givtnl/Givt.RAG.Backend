using backend.business.Backers.Models;
using MediatR;
using NJsonSchema.Annotations;

namespace backend.business.Backers.Commands.Register
{
    /// <summary>
    /// Used to register a follower with a certain participant
    /// </summary>
    public class RegisterBackerCommand : IRequest<BackerDetailModel>
    {
        [JsonSchemaIgnore]
        public string EventId { get; set; }
        [JsonSchemaIgnore]
        public string ParticipantId { get; set; }
        [NotNull]
        public string Name { get; set; }
        [NotNull]
        public string EmailAddress { get; set; }
        [NotNull]
        public decimal Amount { get; set; }
    }
}