using backend.business.Events.Models;
using MediatR;

namespace backend.business.Events.Queries.GetDetail
{
    /// <summary>
    /// Returns the details for a specific event
    /// </summary>
    public class GetEventDetailQuery : IRequest<EventDetailModel>
    {
        public string Id { get; set; }
    }
}