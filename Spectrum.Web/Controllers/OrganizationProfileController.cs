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

         // GET: OrganizationProfile
        public ActionResult OrganizationProfileIndex(int id)
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
