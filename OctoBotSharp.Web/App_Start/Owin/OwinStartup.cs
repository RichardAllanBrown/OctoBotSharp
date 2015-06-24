using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(OctoBotSharp.Web.App_Start.Owin.OwinStartup))]
namespace OctoBotSharp.Web.App_Start.Owin
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/auth"),
            });

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: ""
            //);
        }
    }
}
