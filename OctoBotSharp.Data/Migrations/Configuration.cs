using OctoBotSharp.Data.Identity;
using OctoBotSharp.Domain;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace OctoBotSharp.Data.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<OctoBotSharp.Data.OctoDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(OctoBotSharp.Data.OctoDbContext context)
        {
            var roles = AuthRole.GetAll();

            AddNewRoles(context, roles);
            AddDefaultAdminUser(context, roles);
            AddDefaultGlobalConfig(context);
        }

        private void AddNewRoles(OctoDbContext context, IEnumerable<string> roles)
        {
            var roleManager = new OctoRoleManager(new OctoRoleStore(context));

            foreach (var role in roles)
            {
                if (!roleManager.RoleExists(role))
                    roleManager.Create(OctoRole.Create(role));
            }

            context.SaveChanges();
        }

        private void AddDefaultAdminUser(OctoDbContext context, IEnumerable<string> adminRoles)
        {
            var userManager = OctoUserManager.Create(new OctoUserStore(context), null);

            var adminUser = userManager.FindByName("Admin");
            if (adminUser == null)
            {
                var password = "Abc12345";
                var adminEmail = "email_addr@email.com";

                adminUser = new OctoUser()
                {
                    Email = adminEmail,
                    UserName = "Admin",
                    EmailConfirmed = true,
                };

                userManager.Create(adminUser, password);
            }

            foreach (var role in adminRoles)
                if (!userManager.IsInRole(adminUser.Id, role))
                    userManager.AddToRole(adminUser.Id, role);
        }
        
        private void AddDefaultGlobalConfig(OctoDbContext context)
        {
            var set = context.Set<GlobalConfig>();
            var firstRecord = set.FirstOrDefault();
            if (firstRecord != null)
                return;

            set.Add(GlobalConfig.CreateDefault());
            context.SaveChanges();
        }
    }
}
