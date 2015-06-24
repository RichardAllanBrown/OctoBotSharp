using Microsoft.AspNet.Identity.EntityFramework;
using OctoBotSharp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Data.Identity
{
    public class OctoUserStore : UserStore<OctoUser, OctoRole, int, OctoLogin, OctoUserRole, OctoClaim>
    {
        public OctoUserStore(OctoDbContext context)
            : base (context)
        {
        }
    }
}
