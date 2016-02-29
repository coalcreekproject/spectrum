using System;
using System.Web.Mvc;
using AutoMapper;
using Spectrum.Logic.Identity;
using Spectrum.Logic.Models;
using Spectrum.Web.Models;
using System.Threading.Tasks;

namespace Spectrum.Web.Controllers.Web
{
    public class PortalController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangeIdentityFocus(IdentityFocusViewModel identityFocusViewModel)
        {
            return new HttpNotFoundResult();
        }

        public ActionResult Template(string template)
        {
            switch (template.ToLower())
            {
                case "portalindex":
                    return PartialView("~/Views/Portal/Partials/PortalIndex.cshtml");
                case "changeuserfocusmodal":
                    var userModel = UserUtility.GetUserFromMemoryCache(User);
                    return PartialView("~/Views/Portal/Partials/ChangeIdentityFocusModal.cshtml", Mapper.Map<UserViewModel>(userModel));
                default:
                    throw new ApplicationException("Unknown Template");
            }
        }
    }
}