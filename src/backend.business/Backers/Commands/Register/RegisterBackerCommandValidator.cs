using FluentValidation;

namespace backend.business.Backers.Commands.Register
{
    public class RegisterBackerCommandValidator : AbstractValidator<RegisterBackerCommand>
    {
        public RegisterBackerCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.EmailAddress).EmailAddress().NotEmpty();
            RuleFor(x => x.EventId).NotEmpty();
            RuleFor(x => x.ParticipantId).NotEmpty();
            RuleFor(x => x.Amount).GreaterThan(0);
        }
    }
}