using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Data
{
    public class OctoDbContextFactory : IDbContextFactory<OctoDbContext>
    {
        public OctoDbContext Create()
        {
            return new OctoDbContext("OctoBotContextConnection");
        }
    }
}
