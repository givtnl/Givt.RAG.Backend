
using System;
using NJsonSchema.Annotations;

namespace backend.business.Events.Models
{
    public class EventListModel
    {
        [NotNull]
        public string Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        [NotNull]
        public DateTime StartDate { get; set; }
        [NotNull]
        public DateTime EndDate { get; set; }
        [NotNull]
        public string City { get; set; }
    }
}