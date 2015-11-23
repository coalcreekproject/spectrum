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
                name: "Templates",
                url: "Templates/{controller}/{template}",
                defaults: new { action = "Template" },
                namespaces: new[] { "Spectrum.Web.Controllers.Web" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional },
                namespaces: new [] { "Spectrum.Web.Controllers.Web" }
            );
        }
    }
}
