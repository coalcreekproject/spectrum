using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Services.Description;
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
    public class OrganizationProfilesController : ApiController
    {
        private ICoreDbContext _context;
        private IOrganizationProfileRepository _organizationProfileRepository;

        public OrganizationProfilesController(ICoreUnitOfWork uow)
        {
            _context = uow.Context;

            //TODO: Newing this up is no good
            _organizationProfileRepository = new OrganizationProfileRepository(uow);
        }

        // GET: api/OrganizationProfiles
        [System.Web.Http.HttpGet]
        public IEnumerable<OrganizationProfileViewModel> Get()
        {
            var organizationProfileViewModels = new List<OrganizationProfileViewModel>();

            foreach (OrganizationProfile p in _organizationProfileRepository.All)
            {
                organizationProfileViewModels.Add(Mapper.Map<OrganizationProfileViewModel>(p));
            }

            //TODO: Get paging working
            return organizationProfileViewModels;
        }

        [System.Web.Http.HttpGet]
        // GET: api/OrganizationProfiles/5
        public HttpResponseMessage Get(int id)
        {
            var organizationProfiles = _organizationProfileRepository.All.Where(p => p.OrganizationId == id);
            var organizationProfileViewModels = new List<OrganizationProfileViewModel>();

            foreach (var p in organizationProfiles)
            {
                organizationProfileViewModels.Add(Mapper.Map<OrganizationProfileViewModel>(p));
            }

            return Request.CreateResponse(HttpStatusCode.OK, organizationProfileViewModels);
        }

        // POST: api/OrganizationProfiles
        public async Task<HttpResponseMessage> Post([FromBody]OrganizationProfileViewModel newOrganizationProfile)
        {
            var organizationProfile = Mapper.Map<OrganizationProfile>(newOrganizationProfile);

            organizationProfile.ObjectState = ObjectState.Added;
            _organizationProfileRepository.InsertOrUpdate(organizationProfile);
            var result = Task.FromResult(_organizationProfileRepository.SaveAsync());

            if (result.IsCompleted)
            {
                return Request.CreateResponse(HttpStatusCode.Created,
                    organizationProfile);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // PUT: api/OrganizationProfiles/5
        public HttpResponseMessage Put(int id, [FromBody]OrganizationProfileViewModel editOrganizationProfile)
        {
            var organizationProfile = _organizationProfileRepository.FindAsync(editOrganizationProfile.Id).Result;

            if (organizationProfile == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            Mapper.Map(editOrganizationProfile, organizationProfile);

            //TODO: Examine this graph more closely, namely drill down into the xtension method that chanages the EntityState based on the ObjectState value.
            organizationProfile.ObjectState = ObjectState.Modified;
            //organizationProfile.Organization.ObjectState = ObjectState.Unchanged;
            
            _organizationProfileRepository.InsertOrUpdate(organizationProfile);
            _organizationProfileRepository.SaveAsync();

            return Request.CreateResponse(HttpStatusCode.OK, organizationProfile);
        }

        // DELETE: api/OrganizationProfiles/5
        public HttpResponseMessage Delete(int id)
        {
            var organizationProfile = _organizationProfileRepository.FindAsync(id).Result;

            if (organizationProfile == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            organizationProfile.ObjectState = ObjectState.Deleted;
            _organizationProfileRepository.Delete(organizationProfile.Id);
            _organizationProfileRepository.SaveAsync();

            return Request.CreateResponse(HttpStatusCode.OK, organizationProfile);
        }
    }
}
