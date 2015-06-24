using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using OctoBotSharp.Domain;
using System.Data.Entity.ModelConfiguration;

namespace OctoBotSharp.Data.DbMapping
{
    internal class IdentityCustomisationMap
    {
        internal static void Register(DbModelBuilder modelBuilder)
        {
            ConfigureClaim(modelBuilder.Entity<OctoClaim>());
            ConfigureLogin(modelBuilder.Entity<OctoLogin>());
            ConfigureRole(modelBuilder.Entity<OctoRole>());
            ConfigureUser(modelBuilder.Entity<OctoUser>());
            ConfigureUserRole(modelBuilder.Entity<OctoUserRole>());
        }

        private static void ConfigureUserRole(EntityTypeConfiguration<OctoUserRole> config)
        {
        }

        private static void ConfigureUser(EntityTypeConfiguration<OctoUser> config)
        {
            config.Property(p => p.Money).HasPrecision(19, 4);
        }

        private static void ConfigureRole(EntityTypeConfiguration<OctoRole> config)
        {
        }

        private static void ConfigureLogin(EntityTypeConfiguration<OctoLogin> config)
        {
        }

        private static void ConfigureClaim(EntityTypeConfiguration<OctoClaim> config)
        {
        }
    }
}
