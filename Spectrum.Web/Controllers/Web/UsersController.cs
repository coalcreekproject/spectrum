using System.Collections.Generic;
using System.Net;
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
    public class UsersController : Controller
    {
        private CoreDbContext _context;
        private UserRepository _userRepository;
        private readonly UserManager<User, int> _manager;

        public UsersController(ICoreUnitOfWork uow)
        {
            _context = uow.Context;
            _userRepository = new UserRepository(uow);
            _manager = new UserManager<User, int>(_userRepository);
        }

        #region User CRUD

        // GET: User
        public ActionResult UsersPanelIndex()
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

        public ActionResult Master()
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

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = _manager.FindById((int)id);

            if (user == null)
            {
                return HttpNotFound();
            }

            UserViewModel userViewModel = new UserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };

            return View(userViewModel);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,Email,Password,ConfirmPassword")] UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    UserName = userViewModel.UserName,
                    Email = userViewModel.Email,
                };

                _manager.CreateAsync(user, userViewModel.Password);

                return RedirectToAction("Index");
            }

            return View(userViewModel);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = _manager.FindById((int)id);

            if (user == null)
            {
                return HttpNotFound();
            }

            UserViewModel userViewModel = new UserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName
            };

            return View(userViewModel);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Email,Password,ConfirmPassword")] UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = _manager.FindById(userViewModel.Id);

                user.Email = userViewModel.Email;
                _manager.Update(user);

                if (!string.IsNullOrEmpty(userViewModel.Password))
                {
                    //save password
                    _manager.RemovePassword(user.Id);
                    _manager.AddPassword(user.Id, userViewModel.Password);
                }

                return RedirectToAction("Index");
            }
            return View(userViewModel);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = _manager.FindById((int)id);

            if (user == null)
            {
                return HttpNotFound();
            }

            UserViewModel userViewModel = new UserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
            };

            return View(userViewModel);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _manager.Delete(_manager.FindById(id));
            return RedirectToAction("Index");
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
