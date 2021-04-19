

using System;
using Amazon.DynamoDBv2.DataModel;

namespace backend.domain
{
    [DynamoDBTable("Events")]
    public class Event
    {
        [DynamoDBHashKey]
        public string DomainType { get; set; }
        [DynamoDBRangeKey]
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
    }
}
