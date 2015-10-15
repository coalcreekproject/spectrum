using System;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;
using Spectrum.Core.Data.Context;
using Spectrum.Core.Data.Context.UnitOfWork;
using Spectrum.Core.Data.Models;
using Spectrum.Core.Data.Repositories;
using Spectrum.Web.Controllers;

namespace Spectrum.Web
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
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
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();
            //container.RegisterType<IUserStore<User, int>, UserStore>(new InjectionConstructor(typeof(SpectrumCoreContext)));
            //container.RegisterType<AccountController>(new InjectionConstructor());
            //container.RegisterType<UserManager<User, int>>(new HierarchicalLifetimeManager());
            //container.RegisterType<AccountController>(new InjectionConstructor());
            //container.RegisterType<IAuthenticationManager>(
            //    new InjectionFactory(o => HttpContext.Current.GetOwinContext().Authentication));
            //container.RegisterType<DbContext, SpectrumCoreContext>(new HierarchicalLifetimeManager());
            
            container.RegisterType<ICoreUnitOfWork, CoreUnitOfWork>();
            container.RegisterType<UserManager<User, int>>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserStore<User, int>, UserRepository>(new InjectionConstructor(typeof(CoreDbContext)));
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());
        }
    }
}
