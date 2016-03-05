using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spectrum.Web.Controllers.Web
{
    public class FileManagerController : Controller
    {
        // GET: FileManager
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Template(string template)
        {
            switch (template.ToLower())
            {
                case "filemanagerindex":
                    return PartialView("~/Views/FileManager/Partials/FileManagerIndex.cshtml");
                default:
                    throw new ApplicationException("Unknown Template");
            }
        }
    }
}