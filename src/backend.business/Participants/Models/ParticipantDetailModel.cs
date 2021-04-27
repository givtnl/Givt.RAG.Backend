using System;
using System.Collections.Generic;
using NJsonSchema.Annotations;

namespace backend.business.Participants.Models
{
    public class ParticipantDetailModel
    {
        public ParticipantDetailModel()
        {
            Targets = new List<EventTargetDetailModel>();
        }
        [NotNull]
        public string Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        public string EntryNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public decimal? DistanceInMeters { get; set; }
        public string Status { get; set; }
        public List<EventTargetDetailModel> Targets { get; set; }
    }
}