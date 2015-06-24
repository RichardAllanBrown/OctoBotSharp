using Microsoft.AspNet.Identity.EntityFramework;
using OctoBotSharp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Data.Identity
{
    public class OctoRoleStore : RoleStore<OctoRole, int, OctoUserRole>
    {
        public OctoRoleStore(OctoDbContext context)
            : base(context)
        {
        }

        public OctoRole FindByName(string role)
        {
            return FindByNameAsync(role).Result;
        }

        public bool Exists(string role)
        {
            return FindByName(role) != null;
        }

        public void Create(OctoRole octoRole)
        {
            CreateAsync(octoRole).RunSynchronously();
        }
    }
}
