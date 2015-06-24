using AutoMapper;
using OctoBotSharp.Domain;
using OctoBotSharp.Web.Models.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctoBotSharp.Web.Mappings
{
    public class MembersMap : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<OctoUser, UserSummary>()
                .ForMember(d => d.Id, a => a.MapFrom(s => s.Id))
                .ForMember(d => d.CurrentWealth, a=> a.MapFrom(s => s.Money))
                .ForMember(d => d.UserName, a => a.MapFrom(s => s.UserName))
                ;
        }
    }
}