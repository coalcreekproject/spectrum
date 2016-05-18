using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spectrum.Web.Areas.Eoc.Controllers.Web
{
    public class SignificantEventsController : Controller
    {
        // GET: Eoc/SignificantEvents
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Template(string template)
        {
            const string basePath = "~/Areas/Eoc/Views/SignificantEvents/Partials/";

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