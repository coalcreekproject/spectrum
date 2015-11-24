using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Spectrum.Core.Data.Models;
using Spectrum.Logic.Models;
using Spectrum.Web.Models;

namespace Spectrum.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserModel>();
                cfg.CreateMap<UserModel, User>();

                cfg.CreateMap<UserProfile, UserProfileModel>();
                cfg.CreateMap<UserProfileModel, UserProfile>();

                cfg.CreateMap<UserProfile, UserProfileViewModel>();
                cfg.CreateMap<UserProfileViewModel, UserProfile>();

                cfg.CreateMap<OrganizationProfile, OrganizationProfileViewModel>();
                cfg.CreateMap<OrganizationProfileViewModel, OrganizationProfile>()
                    .ForMember(p => p.Organization, o => o.Ignore());

                cfg.CreateMap<Role, RoleViewModel>();
                cfg.CreateMap<RoleViewModel, Role>();


            });

        }
    }
}
