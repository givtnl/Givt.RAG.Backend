using System.Threading;
using System.Threading.Tasks;
using backend.business.Backers.Models;
using backend.business.Infrastructure;
using MediatR;
using Mollie.Api.Client;
using Mollie.Api.Models;
using Mollie.Api.Models.Payment.Request;

namespace backend.business.Backers.Commands.Checkout
{
    public class CheckoutBackerCommandHandler : IRequestHandler<CheckoutBackerCommand, BackerPaymentRequestModel>
    {
        private readonly ApplicationSettings _applicationSettings;

        public CheckoutBackerCommandHandler(ApplicationSettings applicationSettings)
        {
            _applicationSettings = applicationSettings;
        }
        public async Task<BackerPaymentRequestModel> Handle(CheckoutBackerCommand request, CancellationToken cancellationToken)
        {
            var molliePaymentClient = new PaymentClient(_applicationSettings.PaymentProviderApiKey);
            var paymentResponse = await molliePaymentClient.CreatePaymentAsync(new PaymentRequest
            {
                Description = request.Description,
                Amount = new Amount(request.Currency, request.TotalAmount),
                RedirectUrl = request.RedirectUrl,
                WebhookUrl = "https://google.be"
            }, true);

            return new BackerPaymentRequestModel
            {
                CheckoutUrl = paymentResponse.Links.Checkout.Href
            };
        }
    }
}