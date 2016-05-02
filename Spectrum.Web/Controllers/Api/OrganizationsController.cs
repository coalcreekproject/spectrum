using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Spectrum.Data.Core.Context.Interfaces;
using Spectrum.Data.Core.Context.UnitOfWork;
using Spectrum.Data.Core.Models;
using Spectrum.Data.Core.Models.Interfaces;
using Spectrum.Data.Core.Repositories;
using Spectrum.Data.Core.Repositories.Interfaces;
using Spectrum.Web.Models;

namespace Spectrum.Web.Controllers.Api
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

        [HttpGet]
        // GET: api/Organization
        public IEnumerable<OrganizationViewModel> Get()
        {
            var organizationViewModels = new List<OrganizationViewModel>();

            foreach (Organization o in _organizationRepository.All)
            {
                organizationViewModels.Add(Mapper.Map<OrganizationViewModel>(o));
            }

            //TODO: Get Paging working
            return organizationViewModels;
        }

        // GET: api/Organization/5
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            var organization = _organizationRepository.FindAsync(id).Result;

            if (organization == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<OrganizationViewModel>(organization));
        }

        // POST: api/Organization
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody]OrganizationViewModel newOrganization)
        {
            Organization organization = new Organization();

            Mapper.Map(newOrganization, organization);

            organization.ObjectState = ObjectState.Added;
            _organizationRepository.InsertOrUpdate(organization);
            await _organizationRepository.SaveAsync();

            return Request.CreateResponse(HttpStatusCode.Created, Mapper.Map<OrganizationViewModel>(organization));
        }

        // PUT: api/Organization/5
        [HttpPut]
        public async Task<HttpResponseMessage> Put([FromBody]OrganizationViewModel editOrganization)
        {
            var organization = _organizationRepository.Find(editOrganization.Id);

            if (organization == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            Mapper.Map(editOrganization, organization);

            organization.ObjectState = ObjectState.Modified;
            _organizationRepository.InsertOrUpdate(organization);
            await _organizationRepository.SaveAsync();

            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<OrganizationViewModel>(organization));
        }

        // DELETE: api/Organization/5
        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            Organization organization = _organizationRepository.Find(id);

            if (organization == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            organization.ObjectState = ObjectState.Deleted;
            _organizationRepository.Delete(organization.Id);

            await _organizationRepository.SaveAsync();

            return Request.CreateResponse(HttpStatusCode.OK,
                Mapper.Map<OrganizationViewModel>(organization));
        }
    }
}
