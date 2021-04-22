using System;
using AutoMapper;
using backend.business.Participants.Commands.Register;
using backend.business.Participants.Models;
using backend.domain;

namespace backend.business.Participants.Mappers
{
    public class ParticipantMapper : Profile
    {
        public ParticipantMapper()
        {

            CreateMap<RegisterParticipantCommand, Participant>()
                .ForMember(x => x.DomainType, c => c.MapFrom(d => nameof(Participant)))
                .ForMember(x => x.Status, c => c.MapFrom(d => ParticipantStatus.Registered))
                .ForMember(x => x.Id, c => c.MapFrom(d => $"{d.EventId}-{DateTime.UtcNow.Ticks}"));
                
            CreateMap<Participant, ParticipantListModel>()
                .ForMember(x => x.EventId, c =>c.MapFrom(d => d.Id.Split("-", StringSplitOptions.RemoveEmptyEntries)[0]))
                .ForMember(x => x.Id, c => c.MapFrom(d => d.Id.Split("-", StringSplitOptions.RemoveEmptyEntries)[1]));

            CreateMap<Participant, ParticipantDetailModel>()
                .ForMember(x => x.Id, c => c.MapFrom(d => d.Id.Split("-", StringSplitOptions.RemoveEmptyEntries)[1]));

            CreateMap<RegisterParticipantGoalCommand, ParticipantGoal>(MemberList.Source);
            CreateMap<ParticipantGoal, ParticipantGoalDetailModel>(MemberList.Source);
        }
    }
}