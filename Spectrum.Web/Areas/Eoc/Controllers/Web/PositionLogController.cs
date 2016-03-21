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
            const string basePath = "~/Areas/Eoc/Views/PositionLog/Partials/";
            var partialView = basePath + template + ".cshtml";
            try
            {
                return PartialView(partialView);
            }
            catch (Exception)
            {
                throw new ApplicationException("Unknown Template");
            }
        }
    }
}