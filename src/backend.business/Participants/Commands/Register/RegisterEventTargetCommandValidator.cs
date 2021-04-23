using FluentValidation;

namespace backend.business.Participants.Commands.Register
{
    public class RegisterEventTargetCommandValidator : AbstractValidator<RegisterEventTargetCommand>
    {
        public RegisterEventTargetCommandValidator()
        {
            RuleFor(x => x.Value).NotEmpty();
        }
    }
}