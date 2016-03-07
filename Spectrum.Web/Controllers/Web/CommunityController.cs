using System;
using System.Web.Mvc;

namespace Spectrum.Web.Controllers.Web
{
    public class CommunityController : Controller
    {
        // GET: Community
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Template(string template)
        {
            switch (template.ToLower())
            {
                case "communityindex":
                    return PartialView("~/Views/Community/Partials/CommunityIndex.cshtml");
                default:
                    throw new ApplicationException("Unknown Template");
            }
        }
    }
}