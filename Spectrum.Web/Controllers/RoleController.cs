using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;
using Spectrum.Core.Data.Context;
using Spectrum.Core.Data.Context.UnitOfWork;
using Spectrum.Core.Data.Models;
using Spectrum.Core.Data.Repositories;
using Spectrum.Web.Models;

namespace Spectrum.Web.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private CoreDbContext _context;
        private RoleRepository _roleRepository;
        public RoleController(CoreUnitOfWork uow)
        {
            _context = uow.Context;
            _roleRepository = new RoleRepository(uow);
        }


        #region Role CRUD

        // GET: OrganizationProfile
        public ActionResult Index(int? id)
        {
            var organization = _context.Organizations.Find(id);

            if (organization == null)
            {
                return HttpNotFound();
            }

            var roles = organization.Roles;
            ViewBag.OrganizationId = organization.Id;

            var roleViewModels = new List<RoleViewModel>();

            foreach (Role r in roles)
            {
                roleViewModels.Add(CreateViewModel(r));
            }

            return View(roleViewModels);
        }

        // GET: Role/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var role = _context.Roles.Find(id);

            if (role == null)
            {
                return HttpNotFound();
            }
            
            return View(CreateViewModel(role));
        }

        // GET: Role/Create
        public ActionResult Create(int? id)
        {
            RoleViewModel roleViewModel = new RoleViewModel() {OrganizationId = (int) id};
            return View(roleViewModel);
        }

        // POST: Role/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,OrganizationId,Description")] RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                _roleRepository.CreateAsync(CreateModel(roleViewModel));

                return RedirectToAction("Index", new RouteValueDictionary(
                    new {controller = "Role", action = "Index", Id = roleViewModel.OrganizationId}));
            }

            return View(roleViewModel);
        }

        // GET: Role/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var task = _roleRepository.FindByIdAsync((int) id);
            
            if (task.Result == null)
            {
                return HttpNotFound();
            }

            RoleViewModel roleViewModel = new RoleViewModel();
            CreateViewModel(task.Result);
            
            return View(roleViewModel);
        }

        // POST: Role/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,OrganizationId,Description")] RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                _roleRepository.UpdateAsync(CreateModel(roleViewModel));
                return RedirectToAction("Index", new {Id = roleViewModel.OrganizationId});
            }
            return View(roleViewModel);
        }

        // GET: Role/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var task = _roleRepository.FindByIdAsync((int)id);

            if (task.Result == null)
            {
                return HttpNotFound();
            }

            return View(CreateViewModel(task.Result));
        }

        // POST: Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var findTask = _roleRepository.FindByIdAsync((int)id);
            _roleRepository.DeleteAsync(findTask.Result);

            return RedirectToAction("Index", new { id = findTask.Result.OrganizationId });
        }

        #endregion

        #region Helpers
        //TODO: Break these out to logic

        private RoleViewModel CreateViewModel(Role role)
        {
            return new RoleViewModel()
            {
                Id = role.Id,
                ApplicationId = role.ApplicationId,
                Description = role.Description,
                Name = role.Name,
                OrganizationId = role.OrganizationId
            };
        }

        private Role CreateModel(RoleViewModel roleViewModel)
        {
            return new Role()
            {
                Id = roleViewModel.Id,
                ApplicationId = roleViewModel.ApplicationId,
                Description = roleViewModel.Description,
                Name = roleViewModel.Name,
                OrganizationId = roleViewModel.OrganizationId
            };
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
