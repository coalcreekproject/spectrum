using System.Web.Mvc;
using System.Web.Routing;

namespace Spectrum.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "EocReroute",
                url: "Eoc/IncidentManagement",
                defaults: new { controller = "Portal", action = "Index" }
            );

            routes.MapRoute(
                name: "TestReroute",
                url: "Test/{*.}",
                defaults: new {controller="Portal", action = "Index" }
            );

            routes.MapRoute(
                name: "Templates",
                url: "Templates/{controller}/{template}",
                defaults: new { action = "Template" },
                namespaces: new[] { "Spectrum.Web.Controllers.Web" }
            );

#if DEBUG
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Portal", action = "Index", id = UrlParameter.Optional},
                namespaces: new[] {"Spectrum.Web.Controllers.Web"}
                );
#else
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new [] { "Spectrum.Web.Controllers.Web" }
            );
#endif
        }
    }
}
