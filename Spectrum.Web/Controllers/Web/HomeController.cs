using System.Web.Mvc;

namespace Spectrum.Web.Controllers.Web
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}