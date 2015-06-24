using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace OctoBotSharp.Data.DbMapping
{
    internal static class ItemOwnershipHistoryMap
    {
        internal static void Define(EntityTypeConfiguration<Domain.ItemOwnershipHistory> config)
        {
            config.HasRequired(r => r.Receiver)
                .WithMany(f => f.RecievedItemHistory)
                .HasForeignKey(fk => fk.ReceiverId)
                .WillCascadeOnDelete(false);

            config.HasRequired(r => r.ItemInstance)
                .WithMany(f => f.History)
                .HasForeignKey(fk => fk.ItemInstanceId)
                .WillCascadeOnDelete(false);
        }
    }
}
