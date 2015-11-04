using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Spectrum.Core.Data.Context;
using Spectrum.Core.Data.Context.UnitOfWork;
using Spectrum.Core.Data.Models;
using Spectrum.Core.Data.Repositories;
using Spectrum.Web.Models;

namespace Spectrum.Web.Controllers.Web
{
    //[Authorize]
    public class UsersGridController : Controller
    {
        private CoreDbContext _context;
        private UserRepository _userRepository;
        private readonly UserManager<User, int> _manager;

        public UsersGridController(ICoreUnitOfWork uow)
        {
            _context = uow.Context;
            _userRepository = new UserRepository(uow);
            _manager = new UserManager<User, int>(_userRepository);
        }

        #region User CRUD

        // GET: User
        public ActionResult UsersGridIndex()
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

        #endregion

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
