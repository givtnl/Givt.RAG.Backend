using backend.business.Backers.Models;
using MediatR;
using NJsonSchema.Annotations;

namespace backend.business.Backers.Commands.Checkout
{
    /// <summary>
    /// Used to initiate the payment for a particular follower
    /// </summary>
    public class CheckoutBackerCommand : IRequest<BackerPaymentRequestModel>
    {
        [JsonSchemaIgnore]
        public string EventId { get; set; }
        [JsonSchemaIgnore]
        public string ParticipantId { get; set; }
        [JsonSchemaIgnore]
        public string BackerId { get; set; }
        [NotNull]
        public decimal TotalAmount { get; set; }
        [NotNull]
        public string Currency { get; set; }
        [NotNull]
        public string Description { get; set; }

        public string RedirectUrl { get; set; }
        public string Locale { get; set; }
    }
}