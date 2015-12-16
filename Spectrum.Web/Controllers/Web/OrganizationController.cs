using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Spectrum.Data.Core.Context;
using Spectrum.Data.Core.Context.UnitOfWork;
using Spectrum.Data.Core.Models;
using Spectrum.Data.Core.Repositories;
using Spectrum.Web.Models;

namespace Spectrum.Web.Controllers.Web
{
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

        //TODO: 
        public ActionResult Template(string template)
        {
            switch (template.ToLower())
            {
                case "organizationindex":
                    return PartialView("~/Views/Organization/Partials/OrganizationIndex.cshtml");
                case "addorganizationmodal":
                    return PartialView("~/Views/Organization/Partials/AddOrganizationModal.cshtml");
                case "editorganizationmodal":
                    return PartialView("~/Views/Organization/Partials/EditOrganizationModal.cshtml");
                case "deleteorganizationmodal":
                    return PartialView("~/Views/Organization/Partials/DeleteOrganizationModal.cshtml");

                case "organizationprofiles":
                    return PartialView("~/Views/Organization/Partials/OrganizationProfile.cshtml");
                case "addorganizationprofilemodal":
                    return PartialView("~/Views/Organization/Partials/AddOrganizationProfileModal.cshtml");
                case "editorganizationprofilemodal":
                    return PartialView("~/Views/Organization/Partials/EditOrganizationProfileModal.cshtml");
                case "deleteorganizationprofilemodal":
                    return PartialView("~/Views/Organization/Partials/DeleteOrganizationProfileModal.cshtml");

                case "organizationroles":
                    return PartialView("~/Views/Organization/Partials/OrganizationRole.cshtml");
                case "addorganizationrolemodal":
                    return PartialView("~/Views/Organization/Partials/AddOrganizationRoleModal.cshtml");
                case "editorganizationrolemodal":
                    return PartialView("~/Views/Organization/Partials/EditOrganizationRoleModal.cshtml");
                case "deleteorganizationrolemodal":
                    return PartialView("~/Views/Organization/Partials/DeleteOrganizationRoleModal.cshtml");

                default:
                    throw new ApplicationException("Unknown Template");
            }
        }

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
