using OctoBotSharp.Web.App_Start.GlobalEventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OctoBotSharp.Web.App_Start
{
    public class FrameworkInit : IRunAtStartup
    {
        public void Execute()
        {
            AreaRegistration.RegisterAllAreas();

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}