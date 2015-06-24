using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace OctoBotSharp.Data.DbMapping
{
    internal static class TransactionMap
    {
        internal static void Define(EntityTypeConfiguration<Domain.Transaction> config)
        {
            config.HasOptional(p => p.CreditedUser)
                .WithMany(r => r.Credits)
                .HasForeignKey(fk => fk.CreditedUserId);

            config.HasOptional(p => p.DebitedUser)
                .WithMany(r => r.Debits)
                .HasForeignKey(fk => fk.DebitedUserId);

            config.Property(p => p.Amount).HasPrecision(19, 4);
        }
    }
}
