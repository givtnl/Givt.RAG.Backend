using System;
using AutoMapper;
using backend.business.Backers.Models;
using backend.domain;

namespace backend.business.Backers.Mappers
{
    public class BackerMapper : Profile
    {
        public BackerMapper()
        {
            CreateMap<Backer, BackerListModel>()
                .ForMember(x => x.Id, c => c.MapFrom(d => d.Id.Split("-", StringSplitOptions.RemoveEmptyEntries)[2]));

            CreateMap<Backer, BackerDetailModel>()
                .ForMember(x => x.Id, c => c.MapFrom(d => d.Id.Split("-", StringSplitOptions.RemoveEmptyEntries)[2]));
        }
    }
}