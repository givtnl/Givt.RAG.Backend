using FluentValidation;

namespace backend.business.Participants.Commands.Finish
{
    public class FinishParticipantCommandValidator : AbstractValidator<FinishParticipantCommand>
    {
        public FinishParticipantCommandValidator()
        {
            RuleFor(x => x.EventId).NotEmpty();
            RuleFor(x => x.ParticipantId).NotEmpty();
        }
    }
}