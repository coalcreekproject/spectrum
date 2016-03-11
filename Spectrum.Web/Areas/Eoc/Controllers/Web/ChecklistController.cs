using System;
using System.Web.Mvc;

namespace Spectrum.Web.Areas.Eoc.Controllers.Web
{
    public class CheckListController : Controller
    {
        // GET: Eoc/Checklist
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Template(string template)
        {
            switch (template.ToLower())
            {
                case "checklistindex":
                    return PartialView("~/Areas/Eoc/Views/Checklist/Partials/CheckListIndex.cshtml");
                default:
                    throw new ApplicationException("Unknown Template");
            }
        }
    }
}