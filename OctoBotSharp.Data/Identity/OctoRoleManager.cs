using Microsoft.AspNet.Identity;
using OctoBotSharp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Data.Identity
{
    public class OctoRoleManager : RoleManager<OctoRole, int>
    {
        public OctoRoleManager(IRoleStore<OctoRole, int> store)
            : base(store)
        {
        }
    }
}
