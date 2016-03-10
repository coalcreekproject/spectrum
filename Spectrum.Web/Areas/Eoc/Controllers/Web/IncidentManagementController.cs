using System;
using System.Web.Mvc;

namespace Spectrum.Web.Areas.Eoc.Controllers.Web
{
    public class IncidentManagementController : Controller
    {
        // GET: Eoc/IncidentManagement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Template(string template)
        {
            switch (template.ToLower())
            {
                case "incidentmanagementindex":
                    return
                        PartialView("~/Areas/Eoc/Views/IncidentManagement/Partials/IncidentManagementIndex.cshtml");
                case "incidentmanagementaddmodal":
                    return
                        PartialView("~/Areas/Eoc/Views/IncidentManagement/Partials/IncidentManagementAddModal.cshtml");
                case "incidentmanagementeditmodal":
                    return
                        PartialView("~/Areas/Eoc/Views/IncidentManagement/Partials/IncidentManagementEditModal.cshtml");
                case "incidentmanagementdeletemodal":
                    return
                        PartialView("~/Areas/Eoc/Views/IncidentManagement/Partials/IncidentManagementDeleteModal.cshtml");
                default:
                    throw new ApplicationException("Unknown Template");
            }
        }
    }
}