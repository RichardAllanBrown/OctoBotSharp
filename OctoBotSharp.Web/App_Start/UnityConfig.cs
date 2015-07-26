using System;
using Microsoft.Practices.Unity;
using OctoBotSharp.Web.App_Start.GlobalEventing;
using OctoBotSharp.Data;
using OctoBotSharp.Data.UnitOfWork;
using OctoBotSharp.Data.Repository;
using OctoBotSharp.Data.Identity;
using Microsoft.AspNet.Identity;
using OctoBotSharp.Domain;
using Microsoft.Owin.Security;
using System.Web;
using System.Reflection;
using OctoBotSharp.Service;
using OctoBotSharp.Service.Parser;
using OctoBotSharp.Service.Interp.Core;

namespace OctoBotSharp.Web.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // Weblayer registrations
            container.RegisterType<IRunAtStartup, FrameworkInit>("run-at-start-framework", new ContainerControlledLifetimeManager());
            container.RegisterType<IRunAtStartup, FluentValidationInit>("run-at-start-fluent", new ContainerControlledLifetimeManager());
            container.RegisterType<IRunAtStartup, AutoMapperInit>("run-at-start-automapper", new ContainerControlledLifetimeManager());

            // Datalayer registrations
            container.RegisterType<OctoDbContext>(new PerRequestLifetimeManager(), new InjectionFactory((x) => new OctoDbContext("OctoBotContextConnection")));
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>), new PerRequestLifetimeManager());

            // Service registrations
            container.RegisterType(typeof(IService<>), typeof(Service<>), new PerRequestLifetimeManager());
            container.RegisterType<ITransactionService, TransactionService>(new PerRequestLifetimeManager());
            container.RegisterType<IGlobalConfigService, GlobalConfigService>(new PerRequestLifetimeManager());

            // Identity framework registrations (srsly Microsoft...)
            container.RegisterType<IRoleStore<OctoRole, int>, OctoRoleStore>(new PerRequestLifetimeManager());
            container.RegisterType<IUserStore<OctoUser, int>, OctoUserStore>(new PerRequestLifetimeManager());
            container.RegisterType<IIdentityMessageService, IdentityEmailService>("identity-message-email", new ContainerControlledLifetimeManager());
            container.RegisterType<OctoUserManager>(new PerRequestLifetimeManager(),
                new InjectionFactory((x) => OctoUserManager.Create(x.Resolve<IUserStore<OctoUser, int>>(), x.Resolve<IIdentityMessageService>("identity-message-email")))
            );
            container.RegisterType<OctoRoleManager>(new PerRequestLifetimeManager());
            container.RegisterType<IAuthenticationManager>(new PerRequestLifetimeManager(), new InjectionFactory((x) => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<OctoSignInManager>(new PerRequestLifetimeManager());

            // FluentValidation validators
            FluentValidation.AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly()).ForEach(result =>
                container.RegisterType(result.InterfaceType, result.ValidatorType, new ContainerControlledLifetimeManager())
            );

            // ScriptRunner Registrations
            container.RegisterType<IInterpreter, Interpreter>(new PerRequestLifetimeManager());
            container.RegisterType<ILexer>(new PerRequestLifetimeManager(), new InjectionFactory(x => Lexer.CreateDefault()));
            container.RegisterType<IParser, Parser>(new PerRequestLifetimeManager());
            container.RegisterType<IExecutor, Executor>(new PerRequestLifetimeManager());
            container.RegisterType<IFunctionFactory>(new ContainerControlledLifetimeManager(), 
                new InjectionFactory(x => FunctionFactory.Create(type => UnityConfig.GetConfiguredContainer().Resolve(type))));
        }
    }
}
