using FluentValidation;

namespace backend.business.Participants.Commands.Register
{
    public class RegisterParticipantGoalCommandValidator : AbstractValidator<RegisterParticipantGoalCommand>
    {
        public RegisterParticipantGoalCommandValidator()
        {
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.Value).NotEmpty();
        }
    }
}