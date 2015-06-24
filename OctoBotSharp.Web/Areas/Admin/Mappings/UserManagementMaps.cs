using AutoMapper;
using OctoBotSharp.Domain;
using OctoBotSharp.Web.App_Start;
using OctoBotSharp.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OctoBotSharp.Web.Infrastructure;

namespace OctoBotSharp.Web.Areas.Admin.Mappings
{
    public class UserManagementMap : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<OctoUser, UserSummaryViewModel>()
                .ForMember(d => d.Email, a => a.MapFrom(s => s.Email))
                .ForMember(d => d.EmailConfirmed, a => a.MapFrom(s => s.EmailConfirmed))
                .ForMember(d => d.UserName, a => a.MapFrom(s => s.UserName))
                .ForMember(d => d.Id, a => a.MapFrom(s => s.Id))
                ;

            Mapper.CreateMap<OctoUser, UserDetailViewModel>()
                .IncludeBase<OctoUser, UserSummaryViewModel>()
                .ForMember(d => d.CurrentBalance, a => a.MapFrom(s => s.Money))
                .ForMember(d => d.Permissions, a => a.ResolveUsing<UserPermissionsListResolver>().ConstructedByUnity())
                .ForMember(d => d.Transactions, a => a.ResolveUsing<UserTransactionsResolver>())
                ;
        }
    }
}