using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OctoBotSharp.Domain;
using OctoBotSharp.Data.DbMapping;

namespace OctoBotSharp.Data
{
    public class OctoDbContext : IdentityDbContext<OctoUser, OctoRole, int, OctoLogin, OctoUserRole, OctoClaim>
    {
        static OctoDbContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<OctoDbContext, Migrations.Configuration>());
        }

        public OctoDbContext(string connString)
            : base(connString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            IdentityCustomisationMap.Register(modelBuilder);

            ItemDefinitionMap.Define(modelBuilder.Entity<ItemDefinition>());
            ItemInstanceMap.Define(modelBuilder.Entity<ItemInstance>());
            TransactionMap.Define(modelBuilder.Entity<Transaction>());
            ItemOwnershipHistoryMap.Define(modelBuilder.Entity<ItemOwnershipHistory>());
            GlobalConfigMap.Define(modelBuilder.Entity<GlobalConfig>());
        }
    }
}
