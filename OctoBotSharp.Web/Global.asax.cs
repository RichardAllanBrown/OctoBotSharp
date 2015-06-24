using OctoBotSharp.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using OctoBotSharp.Web.App_Start.GlobalEventing;

namespace OctoBotSharp.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            TaskRunner<IRunAtStartup>();
        }

        protected void Application_BeginRequest()
        {
            TaskRunner<IRunAtBeginRequest>();
        }

        protected void Application_EndRequest()
        {
            TaskRunner<IRunAtEndRequest>();
        }

        private static void TaskRunner<T>() where T : IExecutable
        {
            var tasks = UnityConfig.GetConfiguredContainer().ResolveAll<T>();

            foreach (var task in tasks)
            {
                task.Execute();
            }
        }
    }
}
