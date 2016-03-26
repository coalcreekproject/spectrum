using System.Web.Mvc;

namespace Spectrum.Web.Areas.Eoc
{
    public class EocAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Eoc"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Eoc Templates",
                url: "Eoc/Templates/{controller}/{template}",
                defaults: new {action = "Template"},
                namespaces: new[] {"Spectrum.Web.Areas.Eoc.Controllers.Web"}
                );

            context.MapRoute(
                name: "EocIncident",
                url: "Eoc/IncidentManagement/{action}/{id}",
                defaults: new {action = "Index", controller = "IncidentManagement", id = UrlParameter.Optional},
                namespaces: new[] {"Spectrum.Web.Areas.Eoc.Controllers.Web"}
                );

            context.MapRoute(
                name: "EocPositionLog",
                url: "Eoc/PositionLog/{action}/{id}",
                defaults: new {action = "Index", controller = "PositionLog", id = UrlParameter.Optional},
                namespaces: new[] {"Spectrum.Web.Areas.Eoc.Controllers.Web"}
                );

            context.MapRoute(
                name: "EocIncidentTimeline",
                url: "Eoc/IncidentTimeline/{action}/{id}",
                defaults: new { action = "Index", controller = "IncidentTimeline", id = UrlParameter.Optional },
                namespaces: new[] { "Spectrum.Web.Areas.Eoc.Controllers.Web" }
                );

            context.MapRoute(
                name: "EocChecklists",
                url: "Eoc/CheckList/{action}/{id}",
                defaults: new {action = "Index", controller = "CheckList", id = UrlParameter.Optional},
                namespaces: new[] {"Spectrum.Web.Areas.Eoc.Controllers.Web"}
                );
        }
    }
}