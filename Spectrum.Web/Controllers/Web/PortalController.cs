using System;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Spectrum.Logic.Identity;
using Spectrum.Logic.Models;
using Spectrum.Web.Models;
using System.Threading.Tasks;

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
                    return PartialView("~/Views/Portal/Partials/ChangeIdentityFocusModal.cshtml");
                case "identitypopover":
                    return PartialView("~/Views/Portal/Partials/IdentityPopover.cshtml");
                default:
                    throw new ApplicationException("Unknown Template");
            }
        }
    }
}