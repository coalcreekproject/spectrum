using System.Collections.Generic;
using System.Web.Mvc;
using Spectrum.Core.Data.Context;
using Spectrum.Core.Data.Context.UnitOfWork;
using Spectrum.Core.Data.Models;
using Spectrum.Core.Data.Repositories;
using Spectrum.Web.Models;

namespace Spectrum.Web.Controllers
{
    [Authorize]
    public class OrganizationController : Controller
    {
        private ICoreUnitOfWork _coreUnitOfWork;
        private CoreDbContext _context;
        private OrganizationRepository _organizationRepository;

        public OrganizationController(ICoreUnitOfWork uow)
        {
            _coreUnitOfWork = uow;
            _context = uow.Context;
            _organizationRepository = new OrganizationRepository(uow);
        }

        // GET: Organization
        public ActionResult Index()
        {
            List<OrganizationViewModel> organizationViewModels = new List<OrganizationViewModel>();

            foreach (Organization o in _context.Organizations)
            {
                organizationViewModels.Add(new OrganizationViewModel()
                {
                    Id = o.Id,
                    Name = o.Name,
                    OrganizationTypeId = o.OrganizationTypeId
                });

            }

            return View(organizationViewModels);
        }

        //// GET: Organization/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    Organization organization = _context.Organizations.Find(id);

        //    if (organization == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    OrganizationViewModel organizationViewModel = new OrganizationViewModel()
        //    {
        //        Id = organization.Id,
        //        OrganizationTypeId = organization.OrganizationTypeId,
        //        Name = organization.Name,
        //    };
            
        //    return View(organizationViewModel);
        //}

        //// GET: Organization/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Organization/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Name,Description,OrganizationTypeId")] OrganizationViewModel organizationViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Organization organization = new Organization()
        //        {
        //            Id = organizationViewModel.Id,
        //            Name = organizationViewModel.Name,
        //            OrganizationTypeId = organizationViewModel.OrganizationTypeId
        //        };

        //        _context.Organizations.Add(organization);
        //        _context.SaveChanges();
                
        //        return RedirectToAction("Index");
        //    }

        //    return View(organizationViewModel);
        //}

        //// GET: Organization/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    Organization organization = _context.Organizations.Find(id);

        //    if (organization == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    OrganizationViewModel organizationViewModel = new OrganizationViewModel()
        //    {
        //        Id = organization.Id,
        //        Name = organization.Name,
        //        OrganizationTypeId = organization.OrganizationTypeId
        //    };
            

        //    return View(organizationViewModel);
        //}

        //// POST: Organization/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Name,Description,OrganizationTypeId")] OrganizationViewModel organizationViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Organization organization = new Organization()
        //        {
        //            Id = organizationViewModel.Id,
        //            Name = organizationViewModel.Name,
        //            OrganizationTypeId = organizationViewModel.OrganizationTypeId
        //        };

        //        //_context.Organizations.AddOrUpdate(organization);
        //        _context.SaveChanges();

        //        return RedirectToAction("Index");
        //    }
        //    return View(organizationViewModel);
        //}

        //// GET: Organization/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    Organization organization = _context.Organizations.Find(id);

        //    if (organization == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    OrganizationViewModel organizationViewModel = new OrganizationViewModel()
        //    {
        //        Id = organization.Id,
        //        Name = organization.Name,
        //        OrganizationTypeId = organization.OrganizationTypeId
        //    };

        //    return View(organizationViewModel);
        //}

        //// POST: Organization/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Organization organization = _context.Organizations.Find(id);
        //    _context.Organizations.Remove(organization);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _coreUnitOfWork.Dispose();
                _context.Dispose();
                _organizationRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
