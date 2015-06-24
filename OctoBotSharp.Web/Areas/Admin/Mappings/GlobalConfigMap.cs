using AutoMapper;
using OctoBotSharp.Domain;
using OctoBotSharp.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctoBotSharp.Web.Areas.Admin.Mappings
{
    public class GlobalConfigMap : Profile
    {
        public GlobalConfigMap()
        {
            Mapper.CreateMap<GlobalConfig, GlobalConfigViewModel>()
                .ForMember(d => d.MoneyChar, a => a.MapFrom(s => s.CurrenyChar))
                ;

            Mapper.CreateMap<GlobalConfigViewModel, GlobalConfig>()
                .ForMember(d => d.CurrenyChar, a => a.MapFrom(s => s.MoneyChar))
                ;
        }
    }
}
