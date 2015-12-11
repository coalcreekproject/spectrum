using System.Web.Mvc;

namespace Spectrum.Web.Areas.Eoc
{
    public class EocAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Eoc";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Eoc Templates",
                "Eoc/Templates/{controller}/{action}/{id}",
                new {action = "Index", id = UrlParameter.Optional},
                new[] {"Spectrum.Web.Areas.Eoc.Controllers"}
                );

            context.MapRoute(
                "Eoc",
                "Eoc/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "Spectrum.Web.Areas.Eoc.Controllers" }
            );
        }
    }
}