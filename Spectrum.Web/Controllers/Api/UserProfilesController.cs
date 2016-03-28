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
    public class UserProfilesController : ApiController
    {
        private ICoreDbContext _context;
        private IUserProfileRepository _userProfileRepository;

        public UserProfilesController(ICoreUnitOfWork unitOfWork)
        {
            _context = unitOfWork.Context;
            _userProfileRepository = new UserProfileRepository(unitOfWork);
        }

        [HttpGet]
        // GET: api/UserProfiles/5
        public HttpResponseMessage Get(int id)
        {
            var userProfiles = _userProfileRepository.All.Where(p => p.UserId == id);
            var userProfileViewModels = new List<UserProfileViewModel>();

            foreach (var p in userProfiles)
            {
                userProfileViewModels.Add(Mapper.Map<UserProfileViewModel>(p));
            }

            return Request.CreateResponse(HttpStatusCode.OK, userProfileViewModels);
        }

        [HttpGet]
        // GET: api/Roles/5
        public HttpResponseMessage Get(int profileId, int userId)
        {
            var userProfile = _userProfileRepository.Find(profileId);

            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<UserProfileViewModel>(userProfile));
        }

        // POST: api/UserProfiles
        public async Task<HttpResponseMessage> Post([FromBody] UserProfileViewModel newUserProfile)
        {
            var userProfile = Mapper.Map<UserProfile>(newUserProfile);

            userProfile.ObjectState = ObjectState.Added;
            _userProfileRepository.InsertOrUpdate(userProfile);
            await _userProfileRepository.SaveAsync();

            return Request.CreateResponse(HttpStatusCode.Created,
                Mapper.Map<UserProfileViewModel>(userProfile));
        }
        
        // PUT: api/UserProfiles/5
        [HttpPut]
        public async Task<HttpResponseMessage> Put([FromBody]UserProfileViewModel editedProfile)
        {
            var userProfile = _userProfileRepository.FindAsync(editedProfile.Id).Result;

            if (userProfile == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            Mapper.Map(editedProfile, userProfile);

            userProfile.ObjectState = ObjectState.Modified;
            _userProfileRepository.InsertOrUpdate(userProfile);
            await _userProfileRepository.SaveAsync();

            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<UserProfileViewModel>(userProfile));
        }

        // DELETE: api/UserProfiles/5
        public async Task<HttpResponseMessage> Delete(int id)
        {
            var userProfile = _userProfileRepository.Find(id);

            if (userProfile == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            userProfile.ObjectState = ObjectState.Deleted;
            _userProfileRepository.Delete(userProfile.Id);
            await _userProfileRepository.SaveAsync();

            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<UserProfileViewModel>(userProfile));
        }
    }
}
