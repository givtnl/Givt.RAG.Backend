using System.Collections.Generic;
using backend.business.Events.Models;
using MediatR;

namespace backend.business.Events.Queries.GetList
{
    /// <summary>
    /// used to retrieve a list of events
    /// </summary>
    public class GetEventsListQuery : IRequest<IEnumerable<EventListModel>>
    {
        /// <summary>
        /// Retrieve the events a certain participant has participated in
        /// </summary>
        public long? ParticipantId { get; set; }
    }
}