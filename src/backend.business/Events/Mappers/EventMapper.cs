using AutoMapper;
using backend.business.Events.Models;
using backend.domain;

namespace backend.business.Events.Mappers
{
    public class EventMapper : Profile
    {
        public EventMapper()
        {
            CreateMap<Event, EventListModel>(MemberList.Destination);
            CreateMap<Event, EventDetailModel>(MemberList.Destination);
        }
    }
}