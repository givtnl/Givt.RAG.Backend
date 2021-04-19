using FluentValidation;

namespace backend.business.Participants.Commands.Start
{
    public class StartParticipantCommandValidator : AbstractValidator<StartParticipantCommand>
    {
        public StartParticipantCommandValidator()
        {
            RuleFor(x => x.EventId).NotEmpty();
            RuleFor(x => x.ParticipantId).NotEmpty();
        }
    }
}