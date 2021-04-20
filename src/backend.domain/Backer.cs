using System;
using Amazon.DynamoDBv2.DataModel;

namespace backend.domain
{
    [DynamoDBTable("Backers")]
    public class Backer
    {
        [DynamoDBHashKey]
        public string DomainType { get; set; }
        [DynamoDBRangeKey]
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public static Backer BuildForParticipant(string eventId, string participantId, string name, decimal amount, string emailAddress)
        {
            return new()
            {
                Amount = amount,
                DomainType = nameof(Backer),
                Id = $"{eventId}-{participantId}-{DateTime.UtcNow.Ticks}",
                Name = name,
                EmailAddress = emailAddress
            };
        }
    }
}