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
        [HttpGet]
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

        [HttpGet]
        // GET: api/OrganizationProfiles/5
        public HttpResponseMessage Get(int id)
        {
            var organizationProfiles = _organizationProfileRepository.All.Where(p => p.OrganizationId == id);
            var organizationProfileViewModels = new List<OrganizationProfileViewModel>();

            //TODO: Automapper has a syntax to do list/collection coversions, remove the foreach
            foreach (var p in organizationProfiles)
            {
                organizationProfileViewModels.Add(Mapper.Map<OrganizationProfileViewModel>(p));
            }

            return Request.CreateResponse(HttpStatusCode.OK, organizationProfileViewModels);
        }

        [HttpGet]
        // GET: api/Roles/5
        public HttpResponseMessage Get(int profileId, int organizationId)
        {
            var organizationProfile = _organizationProfileRepository.Find(profileId);

            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<OrganizationProfileViewModel>(organizationProfile));
        }

        // POST: api/OrganizationProfiles
        public async Task<HttpResponseMessage> Post([FromBody] OrganizationProfileViewModel newOrganizationProfile)
        {
            var organizationProfile = Mapper.Map<OrganizationProfile>(newOrganizationProfile);

            organizationProfile.ObjectState = ObjectState.Added;
            _organizationProfileRepository.InsertOrUpdate(organizationProfile);
            await _organizationProfileRepository.SaveAsync();

            return Request.CreateResponse(HttpStatusCode.Created, Mapper.Map<OrganizationProfileViewModel>(organizationProfile));
        }

        // PUT: api/OrganizationProfiles/5
        public async Task<HttpResponseMessage> Put([FromBody]OrganizationProfileViewModel editOrganizationProfile)
        {
            var organizationProfile = _organizationProfileRepository.Find(editOrganizationProfile.Id);

            if (organizationProfile == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            Mapper.Map(editOrganizationProfile, organizationProfile);

            organizationProfile.ObjectState = ObjectState.Modified;
            
            _organizationProfileRepository.InsertOrUpdate(organizationProfile);
            await _organizationProfileRepository.SaveAsync();

            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<OrganizationProfileViewModel>(organizationProfile));
        }

        // DELETE: api/OrganizationProfiles/5
        public async Task<HttpResponseMessage> Delete(int id)
        {
            var organizationProfile = _organizationProfileRepository.Find(id);

            if (organizationProfile == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            organizationProfile.ObjectState = ObjectState.Deleted;
            _organizationProfileRepository.Delete(organizationProfile.Id);
            await _organizationProfileRepository.SaveAsync();

            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<OrganizationProfileViewModel>(organizationProfile));
        }
    }
}
