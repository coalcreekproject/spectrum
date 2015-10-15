using System.Collections.Generic;
using System.Linq;
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
    public class OrganizationProfileController : Controller
    {
        private ICoreUnitOfWork _coreUnitOfWork;
        private CoreDbContext _context;
        private OrganizationProfileRepository _organizationProfileRepository; 
        public OrganizationProfileController(ICoreUnitOfWork uow)
        {
            _coreUnitOfWork = uow;
            _context = uow.Context;
            _organizationProfileRepository = new OrganizationProfileRepository(uow);
        }

        #region OrganizationProfile CRUD

        // GET: OrganizationProfile
        public ActionResult Index(int id)
        {
            IEnumerable<OrganizationProfile> organizationProfiles = 
                _organizationProfileRepository.All.Where(p => p.OrganizationId.Equals(id)).ToList();

            var organizaionProfileViewModels = new List<OrganizationProfileViewModel>();

            foreach (OrganizationProfile op in organizationProfiles)
            {
                organizaionProfileViewModels.Add(CreateViewModel(op));
            }

            return View(organizaionProfileViewModels);
        }

        // GET: OrganizationProfile/Details/5
        public ActionResult Details(int id)
        {
            OrganizationProfile organizationProfile = _organizationProfileRepository.Find(id);

            if (organizationProfile == null)
            {
                return HttpNotFound();
            }

            OrganizationProfileViewModel organizationProfileViewModel = CreateViewModel(organizationProfile);
            
            return View(organizationProfileViewModel);
        }

        // GET: OrganizationProfile/Create
        public ActionResult Create(int id)
        {
            OrganizationProfileViewModel organizationProfileViewModel = new OrganizationProfileViewModel();
            organizationProfileViewModel.OrganizationId = id;

            return View(organizationProfileViewModel);
        }

        // POST: OrganizationProfile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrganizationId,Name,Description,StreetAddressOne," +
                                                   "StreetAddressTwo,City,State,Zip,Phone,Fax,Email," +
                                                   "Country,County,TimeZone,DstAdjust,Language")] 
            OrganizationProfileViewModel organizationProfileViewModel)
        {
            if (ModelState.IsValid)
            {
                _organizationProfileRepository.InsertOrUpdate(CreateModel(organizationProfileViewModel));
                _coreUnitOfWork.Save();

                return RedirectToAction("Index", new RouteValueDictionary(
                    new { controller = "OrganizationProfile", action = "Index", Id = organizationProfileViewModel.OrganizationId }));
            }

            return View(organizationProfileViewModel);
        }

        // GET: OrganizationProfile/Edit/5
        public ActionResult Edit(int id)
        {
            OrganizationProfile organizationProfile = _organizationProfileRepository.Find(id);

            if (organizationProfile == null)
            {
                return HttpNotFound();
            }

            return View(CreateViewModel(organizationProfile));
        }

        // POST: OrganizationProfile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrganizationId,Name,Description,StreetAddressOne,StreetAddressTwo,City,State,Zip,Phone,Fax,Default,Email,Country,County,TimeZone,DstAdjust,Language")] OrganizationProfileViewModel organizationProfileViewModel)
        {
            if (ModelState.IsValid)
            {
                _organizationProfileRepository.InsertOrUpdate(CreateModel(organizationProfileViewModel));
                _coreUnitOfWork.Save();

                return RedirectToAction("Index", new RouteValueDictionary(
                    new { controller = "OrganizationProfile", action = "Index", Id = organizationProfileViewModel.OrganizationId }));
            }
            return View(organizationProfileViewModel);
        }

        // GET: OrganizationProfile/Delete/5
        public ActionResult Delete(int id)
        {
            OrganizationProfile organizationProfile = _organizationProfileRepository.Find(id);

            if (organizationProfile == null)
            {
                return HttpNotFound();
            }

            return View(CreateViewModel(organizationProfile));
        }

        // POST: OrganizationProfile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int organizationId = _organizationProfileRepository.Find(id).OrganizationId;
            _organizationProfileRepository.Delete(id);
            _coreUnitOfWork.Save();

            return RedirectToAction("Index", new { id = organizationId });
        }

        #endregion

        #region Helpers

        //TODO: Move these out to somewhere else.

        private OrganizationProfileViewModel CreateViewModel(OrganizationProfile organizationProfile)
        {
            return new OrganizationProfileViewModel()
            {
                Id = organizationProfile.Id,
                OrganizationId = organizationProfile.OrganizationId,
                ProfileName = organizationProfile.ProfileName,
                Description = organizationProfile.Description,
                StreetAddressOne = organizationProfile.StreetAddressOne,
                StreetAddressTwo = organizationProfile.StreetAddressTwo,
                City = organizationProfile.City,
                State = organizationProfile.State,
                Zip = organizationProfile.Zip,
                Phone = organizationProfile.Phone,
                Fax = organizationProfile.Fax,
                Default = organizationProfile.Default,
                Email = organizationProfile.Email,
                Country = organizationProfile.Country,
                County = organizationProfile.County,
                TimeZone = organizationProfile.TimeZone,
                DstAdjust = organizationProfile.DstAdjust,
                Language = organizationProfile.Language
            };
        }

        private OrganizationProfile CreateModel(OrganizationProfileViewModel organizationProfileViewModel)
        {
            return new OrganizationProfile()
            {
                Id = organizationProfileViewModel.Id,
                OrganizationId = organizationProfileViewModel.OrganizationId,
                ProfileName = organizationProfileViewModel.ProfileName,
                Description = organizationProfileViewModel.Description,
                StreetAddressOne = organizationProfileViewModel.StreetAddressOne,
                StreetAddressTwo = organizationProfileViewModel.StreetAddressTwo,
                City = organizationProfileViewModel.City,
                State = organizationProfileViewModel.State,
                Zip = organizationProfileViewModel.Zip,
                Phone = organizationProfileViewModel.Phone,
                Fax = organizationProfileViewModel.Fax,
                Email = organizationProfileViewModel.Email,
                Country = organizationProfileViewModel.Country,
                County = organizationProfileViewModel.County,
                TimeZone = organizationProfileViewModel.TimeZone,
                DstAdjust = organizationProfileViewModel.DstAdjust,
                Language = organizationProfileViewModel.Language
            };
        }

        #endregion


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
                _coreUnitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
