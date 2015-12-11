using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spectrum.Web.Areas.Eoc.Controllers
{
    public class ChecklistController : Controller
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