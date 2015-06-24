using OctoBotSharp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace OctoBotSharp.Data.Identity
{
    public class OctoSignInManager : SignInManager<OctoUser, int>
    {
        public OctoSignInManager(OctoUserManager userManager, IAuthenticationManager authManager)
            : base(userManager, authManager)
        { 
        }
    }
}
