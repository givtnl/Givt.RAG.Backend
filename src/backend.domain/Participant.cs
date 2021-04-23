using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;
using backend.domain.Converters;

namespace backend.domain
{
    [DynamoDBTable("Participants")]
    public class Participant
    {
        public Participant()
        {
            Targets = new List<EventTarget>();
        }

        [DynamoDBHashKey]
        public string DomainType { get; set; }
        [DynamoDBRangeKey]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        [DynamoDBProperty(typeof(DynamoNullableDateTimeConverter))]
        public DateTime? StartDate { get; set; }
        [DynamoDBProperty(typeof(DynamoNullableDateTimeConverter))]
        public DateTime? FinishDate { get; set; }
        public decimal? DistanceInMeters { get; set; }
        public List<EventTarget> Targets { get; set; }
    }
}