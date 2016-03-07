using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spectrum.Web.Controllers.Web
{
    public class HelpController : Controller
    {
        // GET: Help
        public ActionResult Index()
        {
            return View();
        }

        //TODO: 
        public ActionResult Template(string template)
        {
            switch (template.ToLower())
            {
                case "helpindex":
                    return PartialView("~/Views/Help/Partials/HelpIndex.cshtml");
                case "faq":
                    return PartialView("~/Views/Help/Partials/Faq.cshtml");
                default:
                    throw new ApplicationException("Unknown Template");
            }
        }
    }
}