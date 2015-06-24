using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using OctoBotSharp.Data;
using OctoBotSharp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Data.Identity
{
    public class OctoUserManager : UserManager<OctoUser, int>
    {
        protected OctoUserManager(IUserStore<OctoUser, int> store)
            : base (store)
        {
        }

        public static OctoUserManager Create(IUserStore<OctoUser, int> store, IIdentityMessageService emailService)
        {
            var manager = new OctoUserManager(store);

            manager.UserValidator = new UserValidator<OctoUser, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            manager.RegisterTwoFactorProvider("EmailCode",
                new EmailTokenProvider<OctoUser, int>
                {
                    Subject = "Security Code",
                    BodyFormat = "Your security code is: {0}"
                });

            manager.EmailService = emailService;

            var dataProtector = new DpapiDataProtectionProvider("OctoBotSharp");
            manager.UserTokenProvider = new DataProtectorTokenProvider<OctoUser, int>(dataProtector.Create("EmailConfirmation"));

            return manager;
        }
    }
}
