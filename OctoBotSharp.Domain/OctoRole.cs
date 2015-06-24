using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OctoBotSharp.Domain
{
    public class OctoRole : IdentityRole<int, OctoUserRole>
    {
        public static OctoRole Create(string name)
        {
            return new OctoRole()
            {
                Name = name,
            };
        }
    }
}
