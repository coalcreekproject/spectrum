using AutoMapper;
using Spectrum.Core.Data.Context.Interfaces;
using Spectrum.Core.Data.Context.UnitOfWork;
using Spectrum.Core.Data.Models;
using Spectrum.Core.Data.Models.Interfaces;
using Spectrum.Core.Data.Repositories;
using Spectrum.Core.Data.Repositories.Interfaces;
using Spectrum.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Spectrum.Web.Controllers.WebApiControllers
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
                var organizationProfileViewModel = Mapper.Map<OrganizationProfileViewModel>(p);
                organizationProfileViewModels.Add(organizationProfileViewModel);
            }

            //TODO: Get Paging working
            return organizationProfileViewModels;
        }

        [System.Web.Http.HttpGet]
        // GET: api/OrganizationProfiles/5
        public HttpResponseMessage Get(int id)
        {
            var organizationProfile = _organizationProfileRepository.All.Where(p => p.OrganizationId == id);

            if (organizationProfile == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            //var organizationProfileViewModel = Mapper.Map<OrganizationProfileViewModel>(organizationProfile);

            return Request.CreateResponse(HttpStatusCode.OK, organizationProfile);
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
        public HttpResponseMessage Put(int id, [FromBody]OrganizationViewModel editOrganizationProfile)
        {
            var organizationProfile = _organizationProfileRepository.FindAsync(editOrganizationProfile.Id).Result;

            if (organizationProfile == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            organizationProfile = Mapper.Map<OrganizationProfile>(editOrganizationProfile);
            organizationProfile.ObjectState = ObjectState.Modified;
            _organizationProfileRepository.InsertOrUpdate(organizationProfile);
            _organizationProfileRepository.Save();

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
