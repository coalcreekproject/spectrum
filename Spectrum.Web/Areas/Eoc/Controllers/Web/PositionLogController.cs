using System;
using System.Web.Mvc;

namespace Spectrum.Web.Areas.Eoc.Controllers.Web
{
    public class PositionLogController : Controller
    {
        // GET: Eoc/PositionLog
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Template(string template)
        {
            switch (template.ToLower())
            {
                case "positionlogindex":
                    return
                        PartialView("~/Areas/Eoc/Views/PositionLog/Partials/PositionLogIndex.cshtml");
                case "positionlogmanagement":
                    return
                        PartialView("~/Areas/Eoc/Views/PositionLog/Partials/PositionLogManagement.cshtml");
                default:
                    throw new ApplicationException("Unknown Template");
            }
        }
    }
}