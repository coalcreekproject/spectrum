using System;
using System.Web.Mvc;

namespace Spectrum.Web.Controllers.Web
{
    public class PortalController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Template(string template)
        {
            switch (template.ToLower())
            {
                case "portalindex":
                    return PartialView("~/Views/Portal/Partials/PortalIndex.cshtml");
                case "changeuserfocusmodal":
                    return PartialView("~/Views/Portal/Partials/ChangeUserFocusModal.cshtml");
                default:
                    throw new ApplicationException("Unknown Template");
            }
        }
    }
}