using System;
using NJsonSchema.Annotations;

namespace backend.business.Events.Models
{
    /// <summary>
    /// Used to describe an event
    /// </summary>
    public class EventDetailModel
    {
        [NotNull]
        public string Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [NotNull]
        public string City { get; set; }
        [NotNull]
        public string Address { get; set; }
        public string Comment { get; set; }
    }
}