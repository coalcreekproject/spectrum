using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Spectrum.Data.Core.Models;
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
                cfg.CreateMap<Organization, OrganizationViewModel>();
                cfg.CreateMap<OrganizationViewModel, Organization>();

                cfg.CreateMap<Organization, OrganizationModel>();
                cfg.CreateMap<OrganizationModel, Organization>();

                cfg.CreateMap<OrganizationProfile, OrganizationProfileViewModel>();
                cfg.CreateMap<OrganizationProfileViewModel, OrganizationProfile>()
                    .ForMember(p => p.Organization, o => o.Ignore());

                cfg.CreateMap<Position, PositionModel>();
                cfg.CreateMap<PositionModel, Position>();

                cfg.CreateMap<Position, PositionViewModel>()
                    .ForMember(dest => dest.PositionId,
                        opts => opts.MapFrom(src => src.Id));
                cfg.CreateMap<PositionViewModel, Position>()
                    .ForMember(dest => dest.Id,
                        opts => opts.MapFrom(src => src.PositionId));

                cfg.CreateMap<PositionModel, PositionViewModel>()
                    .ForMember(dest => dest.PositionId,
                        opts => opts.MapFrom(src => src.Id));
                cfg.CreateMap<PositionViewModel, PositionModel>()
                    .ForMember(dest => dest.Id,
                        opts => opts.MapFrom(src => src.PositionId));

                cfg.CreateMap<Role, RoleViewModel>()
                    .ForMember(dest => dest.RoleId,
                        opts => opts.MapFrom(src => src.Id));
                cfg.CreateMap<RoleViewModel, Role>()
                    .ForMember(dest => dest.Id,
                        opts => opts.MapFrom(src => src.RoleId));

                cfg.CreateMap<Role, RoleModel>();
                cfg.CreateMap<RoleModel, Role>();

                cfg.CreateMap<RoleModel, RoleViewModel>()
                    .ForMember(dest => dest.RoleId,
                        opts => opts.MapFrom(src => src.Id));
                cfg.CreateMap<RoleViewModel, RoleModel>()
                    .ForMember(dest => dest.Id,
                        opts => opts.MapFrom(src => src.RoleId));

                cfg.CreateMap<User, UserModel>();
                cfg.CreateMap<UserModel, User>();

                cfg.CreateMap<User, UserViewModel>()
                    .ForMember(dest => dest.UserRoles, opt => opt.Ignore())
                    .ForMember(dest => dest.UserPositions, opt => opt.Ignore());
                cfg.CreateMap<UserViewModel, User>();

                cfg.CreateMap<UserModel, UserViewModel>();
                cfg.CreateMap<UserViewModel, UserModel>();

                cfg.CreateMap<UserOrganization, UserOrganizationViewModel>()
                    .ForMember(dest => dest.Name,
                        opts => opts.MapFrom(src => src.Organization.Name));
                cfg.CreateMap<UserOrganizationViewModel, UserOrganization>();

                cfg.CreateMap<UserOrganization, UserOrganizationModel>();
                cfg.CreateMap<UserOrganizationModel, UserOrganization>();

                cfg.CreateMap<UserOrganizationModel, UserOrganizationViewModel>()
                    .ForMember(dest => dest.Name,
                        opts => opts.MapFrom(src => src.Organization.Name));
                cfg.CreateMap<UserOrganizationViewModel, UserOrganizationModel>();

                cfg.CreateMap<UserProfile, UserProfileModel>();
                cfg.CreateMap<UserProfileModel, UserProfile>();

                cfg.CreateMap<UserProfile, UserProfileViewModel>();
                cfg.CreateMap<UserProfileViewModel, UserProfile>();

                cfg.CreateMap<UserProfileModel, UserProfileViewModel>();
                cfg.CreateMap<UserProfileViewModel, UserProfileModel>();
                
                cfg.CreateMap<UserRole, UserRoleViewModel>();
                cfg.CreateMap<UserRoleViewModel, UserRole>();

                cfg.CreateMap<UserRole, UserRoleModel>();
                cfg.CreateMap<UserRoleModel, UserRole>();

                cfg.CreateMap<UserRoleModel, UserRoleViewModel>()
                    .ForMember(dest => dest.Name,
                        opts => opts.MapFrom(src => src.Role.Name));
                cfg.CreateMap<UserRoleViewModel, UserRoleModel>();

                cfg.CreateMap<UserPosition, UserPositionViewModel>();
                cfg.CreateMap<UserPositionViewModel, UserPosition>();

                cfg.CreateMap<UserPosition, UserPositionModel>();
                cfg.CreateMap<UserPositionModel, UserPosition>();

                cfg.CreateMap<UserPositionModel, UserPositionViewModel>()
                    .ForMember(dest => dest.Name,
                        opts => opts.MapFrom(src => src.Position.Name));
                cfg.CreateMap<UserPositionViewModel, UserPositionModel>();

                cfg.CreateMap<OrganizationType, OrganizationTypeViewModel>();
            });
        }
    }
}
