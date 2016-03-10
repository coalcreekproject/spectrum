using System.Web.Mvc;

namespace Spectrum.Web.Areas.Eoc.Controllers.Web
{
    public class DashboardController : Controller
    {
        // GET: Eoc/
        public ActionResult Index()
        {
            return View();
        }

        // GET: Eoc/Dashboard served by Angular
        public ActionResult Dashboard()
        {
            return PartialView();
        }
    }
}