using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace OctoBotSharp.Data.DbMapping
{
    public static class GlobalConfigMap
    {
        internal static void Define(EntityTypeConfiguration<Domain.GlobalConfig> config)
        {
            config.Property(p => p.CurrenyChar).IsUnicode().IsFixedLength().HasMaxLength(1);
        }
    }
}
