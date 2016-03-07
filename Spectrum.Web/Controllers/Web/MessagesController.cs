using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spectrum.Web.Controllers.Web
{
    public class MessagesController : Controller
    {
        // GET: Messages
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Template(string template)
        {
            switch (template.ToLower())
            {
                case "messagesindex":
                    return PartialView("~/Views/Messages/Partials/MessagesIndex.cshtml");
                default:
                    throw new ApplicationException("Unknown Template");
            }
        }
    }
}