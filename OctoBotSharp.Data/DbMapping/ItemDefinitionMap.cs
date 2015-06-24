using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace OctoBotSharp.Data.DbMapping
{
    internal static class ItemDefinitionMap
    {
        internal static void Define(EntityTypeConfiguration<Domain.ItemDefinition> config)
        {
            config.Property(p => p.IrcRenderColor).IsFixedLength().HasMaxLength(6);
            config.Property(p => p.IrcRenderChar).IsUnicode().IsFixedLength().HasMaxLength(1);
        }
    }
}
