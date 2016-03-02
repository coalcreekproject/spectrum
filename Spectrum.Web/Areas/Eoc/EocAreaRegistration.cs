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
                name: "Eoc Templates",
                url: "Eoc/Templates/{controller}/{template}",
                defaults: new {action = "Template"},
                namespaces: new[] {"Spectrum.Web.Areas.Eoc.Controllers.Web"}
                );
        }
    }
}