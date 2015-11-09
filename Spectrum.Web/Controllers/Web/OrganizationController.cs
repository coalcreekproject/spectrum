using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Spectrum.Core.Data.Context;
using Spectrum.Core.Data.Context.UnitOfWork;
using Spectrum.Core.Data.Models;
using Spectrum.Core.Data.Repositories;
using Spectrum.Web.Models;

namespace Spectrum.Web.Controllers.Web
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
        public ActionResult OrganizationIndex()
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
                case "organizationroles":
                    return PartialView("~/Views/Organization/Partials/OrganizationRoles.cshtml");
                case "addorganizationroles":
                    return PartialView("~/Views/Organization/Partials/AddOrganizationRoles.cshtml");
                case "editorganizationroles":
                    return PartialView("~/Views/Organization/Partials/EditOrganizationRoles.cshtml");
                case "deleteorganizationroles":
                    return PartialView("~/Views/Organization/Partials/DeleteOrganizationRoles.cshtml");
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
