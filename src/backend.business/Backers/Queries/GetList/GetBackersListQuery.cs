using System.Collections.Generic;
using backend.business.Backers.Models;
using MediatR;

namespace backend.business.Backers.Queries.GetList
{
    /// <summary>
    /// Used to get a list of followers
    /// </summary>
    public class GetBackersListQuery :  IRequest<IEnumerable<BackerListModel>>
    {
        /// <summary>
        /// Get the followers for a certain event
        /// </summary>
        public string? EventId { get; set; }
        /// <summary>
        /// Get the followers for a specific participant
        /// </summary>
        public string? ParticipantId { get; set; }
    }
}