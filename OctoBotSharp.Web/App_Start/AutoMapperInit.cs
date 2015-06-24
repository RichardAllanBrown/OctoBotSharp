using OctoBotSharp.Web.App_Start.GlobalEventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using OctoBotSharp.Web.Areas.Admin.Mappings;
using OctoBotSharp.Web.Mappings;

namespace OctoBotSharp.Web.App_Start
{
    public class AutoMapperInit : IRunAtStartup
    {
        public void Execute()
        {
            Mapper.AddProfile<UserManagementMap>();
            Mapper.AddProfile<ItemDefinitionMaps>();
            Mapper.AddProfile<GlobalConfigMap>();
            Mapper.AddProfile<MembersMap>();
        }
    }
}