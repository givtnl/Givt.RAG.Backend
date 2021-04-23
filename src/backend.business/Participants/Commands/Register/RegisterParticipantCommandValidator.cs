using FluentValidation;

namespace backend.business.Participants.Commands.Register
{
    public class RegisterParticipantCommandValidator : AbstractValidator<RegisterParticipantCommand>
    {
        public RegisterParticipantCommandValidator()
        {
            RuleFor(x => x.EventId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleForEach(x => x.Targets).SetValidator(x => new RegisterEventTargetCommandValidator());
        }
    }
}