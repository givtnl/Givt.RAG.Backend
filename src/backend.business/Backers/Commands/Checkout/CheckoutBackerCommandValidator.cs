using FluentValidation;

namespace backend.business.Backers.Commands.Checkout
{
    public class CheckoutBackerCommandValidator : AbstractValidator<CheckoutBackerCommand>
    {
        public CheckoutBackerCommandValidator()
        {
            RuleFor(x => x.BackerId).NotEmpty();
            RuleFor(x => x.Currency).NotEmpty();
            RuleFor(x => x.EventId).NotEmpty();
            RuleFor(x => x.ParticipantId).NotEmpty();
            RuleFor(x => x.TotalAmount).GreaterThan(0);
            RuleFor(x => x.RedirectUrl).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}