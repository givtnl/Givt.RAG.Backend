
using NJsonSchema.Annotations;

namespace backend.business.Events.Models
{
    public class EventListModel
    {
        [NotNull]
        public string Id { get; set; }
        [NotNull]
        public string Name { get; set; }
    }
}