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

        [System.Web.Http.HttpGet]
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

        // POST: api/UserProfiles
        public HttpResponseMessage Post([FromBody]UserProfileViewModel newUserProfile)
        {
            var userProfile = Mapper.Map<UserProfile>(newUserProfile);

            userProfile.ObjectState = ObjectState.Added;
            _userProfileRepository.InsertOrUpdate(userProfile);
            var result = Task.FromResult(_userProfileRepository.SaveAsync());

            if (result.IsCompleted)
            {
                return Request.CreateResponse(HttpStatusCode.Created,
                    userProfile);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // PUT: api/UserProfiles/5
        public HttpResponseMessage Put([FromBody]UserProfileViewModel editedProfile)
        {
            var userProfile = _userProfileRepository.FindAsync(editedProfile.Id).Result;

            if (userProfile == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            Mapper.Map(editedProfile, userProfile);

            userProfile.ObjectState = ObjectState.Modified;
            _userProfileRepository.InsertOrUpdate(userProfile);
            _userProfileRepository.SaveAsync();

            return Request.CreateResponse(HttpStatusCode.OK, userProfile);
        }

        // DELETE: api/UserProfiles/5
        public HttpResponseMessage Delete(int id)
        {
            var userProfile = _userProfileRepository.Find(id);

            if (userProfile == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            userProfile.ObjectState = ObjectState.Deleted;
            _userProfileRepository.Delete(userProfile.Id);
            _userProfileRepository.Save();

            return Request.CreateResponse(HttpStatusCode.OK, userProfile);
        }
    }
}
