using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace OctoBotSharp.Data.DbMapping
{
    internal static class ItemInstanceMap
    {
        internal static void Define(EntityTypeConfiguration<Domain.ItemInstance> config)
        {
            config.HasRequired(r => r.Item)
                .WithMany(r => r.OwnedBy)
                .HasForeignKey(fk => fk.ItemId)
                .WillCascadeOnDelete(false);

            config.HasRequired(r => r.Owner)
                .WithMany(r => r.Items)
                .HasForeignKey(fk => fk.OwnerId)
                .WillCascadeOnDelete(false);
        }
    }
}
