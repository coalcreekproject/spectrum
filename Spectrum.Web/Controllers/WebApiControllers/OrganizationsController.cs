using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Spectrum.Core.Data.Context.Interfaces;
using Spectrum.Core.Data.Context.UnitOfWork;
using Spectrum.Core.Data.Models;
using Spectrum.Core.Data.Models.Interfaces;
using Spectrum.Core.Data.Repositories;
using Spectrum.Core.Data.Repositories.Interfaces;
using Spectrum.Web.Models;

namespace Spectrum.Web.Controllers.WebApiControllers
{
    public class OrganizationsController : ApiController
    {
        private ICoreDbContext _context;
        private IOrganizationRepository _organizationRepository;

        public OrganizationsController(ICoreUnitOfWork uow)
        {
            _context = uow.Context;
            _organizationRepository = new OrganizationRepository(uow);
        }

        [System.Web.Http.HttpGet]
        // GET: api/Organization
        public IEnumerable<OrganizationViewModel> Get()
        {
            var organizationViewModels = new List<OrganizationViewModel>();

            foreach (Organization o in _organizationRepository.All)
            {
                organizationViewModels.Add(new OrganizationViewModel
                {
                    Id = o.Id,
                    Name = o.Name,
                    OrganizationTypeId = o.OrganizationTypeId

                });
            }

            //TODO: Get Paging working
            return organizationViewModels;
        }

        // GET: api/Organization/5
        public HttpResponseMessage Get(int id)
        {

            var organization = _organizationRepository.All.FirstOrDefault(u => u.Id == id);

            if (organization == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var organizationViewModel = new OrganizationViewModel
            {
                Id = organization.Id,
                Name = organization.Name,
                OrganizationTypeId = organization.OrganizationTypeId
            };
            
            return Request.CreateResponse(HttpStatusCode.OK, organizationViewModel);
        }

        // POST: api/Organization
        public async Task<HttpResponseMessage> Post([FromBody]OrganizationViewModel newOrganization)
        {
            Organization organization = new Organization
            {
                Id = newOrganization.Id,
                Name = newOrganization.Name,
                OrganizationTypeId = newOrganization.OrganizationTypeId,
                ObjectState = ObjectState.Added
            };

            var result = Task.FromResult(_organizationRepository.CreateOrganization(organization));

            if (result.IsCompleted)
            {
                return Request.CreateResponse(HttpStatusCode.Created,
                    organization);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // PUT: api/Organization/5
        public HttpResponseMessage Put([FromBody]OrganizationViewModel editOrganization)
        {
            var organization = _organizationRepository.FindByIdAsync(editOrganization.Id).Result;

            if (organization == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            organization.Name = editOrganization.Name;
            organization.OrganizationTypeId = editOrganization.OrganizationTypeId;
            organization.ObjectState = ObjectState.Modified;

            _organizationRepository.InsertOrUpdate(organization);
            
            return Request.CreateResponse(HttpStatusCode.OK, organization);
        }

        // DELETE: api/Organization/5
        public HttpResponseMessage Delete(int id)
        {
            Organization organization = _organizationRepository.Find(id);

            if (organization == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            organization.ObjectState = ObjectState.Deleted;
            _organizationRepository.Delete(organization.Id);
            _context.SaveChangesAsync();

            return Request.CreateResponse(HttpStatusCode.OK, organization);
        }
    }
}
