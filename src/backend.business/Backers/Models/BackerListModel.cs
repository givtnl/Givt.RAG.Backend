using NJsonSchema.Annotations;

namespace backend.business.Backers.Models
{
    public class BackerListModel
    {
        [NotNull]
        public string Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        [NotNull]
        public string EmailAddress { get; set; }
          [NotNull]
        public decimal  Amount { get; set; }
    }
}