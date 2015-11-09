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

        //TODO: Complete these views
        public ActionResult Template(string template)
        {
            switch (template.ToLower())
            {
                case "organizationindex":
                    return PartialView("~/Views/Organization/Partials/OrganizationIndex.cshtml");
                case "add":
                    throw new NotImplementedException("Add view not implemented");
                    //return PartialView("~/Views/Organization/Partials/???.cshtml");
                case "edit":
                    throw new NotImplementedException("Edit view not implemented");
                //return PartialView("~/Views/Organization/Partials//???.cshtml");
                case "delete":
                    throw new NotImplementedException("Delete view not implemented");
                //return PartialView("~/Views/Organization/Partials//???.cshtml");
                case "organizationroles":
                    return PartialView("~/Views/Organization/Partials/OrganizationRoles.cshtml");
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
