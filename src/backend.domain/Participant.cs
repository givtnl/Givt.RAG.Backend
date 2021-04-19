﻿using System;
using Amazon.DynamoDBv2.DataModel;

namespace backend.domain
{
    [DynamoDBTable("Participants")]
    public class Participant
    {
        [DynamoDBHashKey]
        public string DomainType { get; set; }
        [DynamoDBRangeKey]
        public string Id { get; set; }
        public string Name { get; set; }
        public static Participant BuildParticipantForEvent(string eventId, string name)
        {
            return new()
            {
                DomainType = nameof(Participant),
                Name = name,
                Id = $"{eventId}-{DateTime.Now.Ticks}"
            };
        }
    }
}