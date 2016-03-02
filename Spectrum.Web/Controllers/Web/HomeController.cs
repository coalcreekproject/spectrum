using System.Web.Mvc;

namespace Spectrum.Web.Controllers.Web
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {
            return PartialView();
        }

        public ActionResult Old()
        {
            return new RedirectResult("~/Content/old/index.html");
        }
    }
}