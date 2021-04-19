using System;
using AutoMapper;
using backend.business.Participants.Models;
using backend.domain;

namespace backend.business.Participants.Mappers
{
    public class ParticipantMapper : Profile
    {
        public ParticipantMapper()
        {
            CreateMap<Participant, ParticipantListModel>()
                .ForMember(x => x.EventId, c =>c.MapFrom(d => d.Id.Split("-", StringSplitOptions.RemoveEmptyEntries)[0]))
                .ForMember(x => x.Id, c => c.MapFrom(d => d.Id.Split("-", StringSplitOptions.RemoveEmptyEntries)[1]));

            CreateMap<Participant, ParticipantDetailModel>()
                .ForMember(x => x.Id, c => c.MapFrom(d => d.Id.Split("-", StringSplitOptions.RemoveEmptyEntries)[1]));
        }
    }
}