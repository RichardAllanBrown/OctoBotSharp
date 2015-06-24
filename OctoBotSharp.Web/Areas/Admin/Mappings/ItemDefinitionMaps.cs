using AutoMapper;
using OctoBotSharp.Domain;
using OctoBotSharp.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctoBotSharp.Web.Areas.Admin.Mappings
{
    public class ItemDefinitionMaps : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<ItemDefinition, ItemDefinitionViewModel>()
                .ForMember(d => d.Id, a => a.MapFrom(s => s.Id))
                .ForMember(d => d.IrcChar, a => a.MapFrom(s => s.IrcRenderChar))
                .ForMember(d => d.IrcColour, a => a.MapFrom(s => s.IrcRenderColor))
                .ForMember(d => d.Name, a => a.MapFrom(s => s.Name))
                .ForMember(d => d.Description, a => a.MapFrom(s => s.Description))
                ;

            Mapper.CreateMap<ItemDefinitionViewModel, ItemDefinition>()
                .ForMember(d => d.CreatedOn, a => a.Ignore())
                .ForMember(d => d.Description, a => a.MapFrom(s => s.Description))
                .ForMember(d => d.IrcRenderChar, a => a.MapFrom(s => s.IrcChar))
                .ForMember(d => d.IrcRenderColor, a => a.MapFrom(s => s.IrcColour))
                .ForMember(d => d.Name, a => a.MapFrom(s => s.Name))
                .ForMember(d => d.Id, a => a.MapFrom(s => s.Id))
                ;

            Mapper.CreateMap<ItemDefinition, ItemDefinitionSummaryViewModel>()
                .ForMember(d => d.Id, a => a.MapFrom(s => s.Id))
                .ForMember(d => d.Name, a => a.MapFrom(s => s.Name))
                .ForMember(d => d.Description, a => a.MapFrom(s => s.Description))
                .ForMember(d => d.IrcChar, a => a.MapFrom(s => s.IrcRenderChar))
                .ForMember(d => d.IrcHexColor, a => a.MapFrom(s => s.IrcRenderColor))
                ;

            Mapper.CreateMap<ItemDefinition, ItemDefinitionDetailViewModel>()
                .IncludeBase<ItemDefinition, ItemDefinitionSummaryViewModel>()
                .ForMember(d => d.Owners, a => a.MapFrom(s => s.OwnedBy))
                ;

            Mapper.CreateMap<ItemInstance, OwnershipSummary>()
                .ForMember(d => d.ObtainedAt, a => a.MapFrom(s => s.History.Max(x => x.ReceivedAt)))
                .ForMember(d => d.UserName, a => a.MapFrom(s => s.Owner.UserName))
                ;
        }
    }
}
