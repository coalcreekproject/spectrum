using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Spectrum.Data.Core.Context;
using Spectrum.Data.Core.Context.UnitOfWork;
using Spectrum.Data.Core.Models;
using Spectrum.Data.Core.Repositories;
using Spectrum.Web.Models;

namespace Spectrum.Web.Controllers.Web
{
    //[Authorize]
    public class UserController : Controller
    {
        private CoreDbContext _context;
        private UserRepository _userRepository;
        private readonly UserManager<User, int> _manager;

        public UserController(ICoreUnitOfWork uow)
        {
            _context = uow.Context;
            _userRepository = new UserRepository(uow);
            _manager = new UserManager<User, int>(_userRepository);
        }

        // GET: User
        public ActionResult Index()
        {
            //UserViewModel

            var userViewModels = new List<UserViewModel>();

            foreach (User u in _manager.Users)
            {
                userViewModels.Add(new UserViewModel()
                {
                    Id = u.Id,
                    Email = u.Email,
                    UserName = u.UserName,
                    Password = "********",
                    ConfirmPassword = "********",
                });
            }

            return View(userViewModels);
        }

        //TODO: 
        public ActionResult Template(string template)
        {
            switch (template.ToLower())
            {
                case "userpanelindex":
                    return PartialView("~/Views/User/Partials/UserPanelIndex.cshtml");
                case "usergridindex":
                    return PartialView("~/Views/User/Partials/UserGridIndex.cshtml");
                case "addusermodal":
                    return PartialView("~/Views/User/Partials/AddUserModal.cshtml");
                case "editusermodal":
                    return PartialView("~/Views/User/Partials/EditUserModal.cshtml");
                case "deleteusermodal":
                    return PartialView("~/Views/User/Partials/DeleteUserModal.cshtml");
                case "userprofileindex":
                    return PartialView("~/Views/User/Partials/UserProfileIndex.cshtml");
                case "adduserprofilemodal":
                    return PartialView("~/Views/User/Partials/AddUserProfileModal.cshtml");
                case "edituserprofilemodal":
                    return PartialView("~/Views/User/Partials/EditUserProfileModal.cshtml");
                case "deleteuserprofilemodal":
                    return PartialView("~/Views/User/Partials/DeleteUserProfileModal.cshtml");
                case "assignuserrolesmodal":
                    return PartialView("~/Views/User/Partials/UserRolesModal.cshtml");
                default:
                    throw new ApplicationException("Unknown Template");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
