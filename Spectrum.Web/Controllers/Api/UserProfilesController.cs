using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Spectrum.Core.Data.Context.Interfaces;
using Spectrum.Core.Data.Context.UnitOfWork;
using Spectrum.Core.Data.Models;
using Spectrum.Core.Data.Models.Interfaces;
using Spectrum.Core.Data.Repositories;
using Spectrum.Core.Data.Repositories.Interfaces;

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
            var result = _userProfileRepository.All.Where(p => p.UserId == id);

            //if (result == null)
            //{
            //    return Request.CreateResponse(HttpStatusCode.NotFound);
            //}

            return Request.CreateResponse(HttpStatusCode.OK, result); 
        }

        // POST: api/UserProfiles
        public HttpResponseMessage Post([FromBody]UserProfile userProfile)
        {
            var profile = new UserProfile
            {
                UserId = userProfile.UserId,

                Default = userProfile.Default,
                DstAdjust = userProfile.DstAdjust,
                Language = userProfile.Language,
                ProfileName = userProfile.ProfileName,
                OrganizationId = userProfile.OrganizationId,

                Title = userProfile.Title,
                FirstName = userProfile.FirstName,
                MiddleName = userProfile.MiddleName,
                LastName = userProfile.LastName,
                Nickname = userProfile.Nickname,

                //Organization = userProfile.Organization,
                Position = userProfile.Position,

                SecondaryEmail = userProfile.SecondaryEmail,
                SecondaryPhoneNumber = userProfile.SecondaryPhoneNumber,

                //Photo = userProfile.Photo;
            };

            profile.ObjectState = ObjectState.Added;
            _userProfileRepository.InsertOrUpdate(profile);
            _userProfileRepository.Save();

            return Request.CreateResponse(HttpStatusCode.Created, profile);
        }

        // PUT: api/UserProfiles/5
        public HttpResponseMessage Put([FromBody]UserProfile userProfile)
        {
            var profile = _userProfileRepository.Find(userProfile.Id);

            profile.Default = userProfile.Default;
            profile.DstAdjust = userProfile.DstAdjust;
            profile.Language = userProfile.Language;
            profile.ProfileName = userProfile.ProfileName;
            profile.OrganizationId = userProfile.OrganizationId;

            profile.Title = userProfile.Title;
            profile.FirstName = userProfile.FirstName;
            profile.MiddleName = userProfile.MiddleName;
            profile.LastName = userProfile.LastName;
            profile.Nickname = userProfile.Nickname;

            //profile.Organization = userProfile.Organization;
            profile.Position = userProfile.Position;

            profile.SecondaryEmail = userProfile.SecondaryEmail;
            profile.SecondaryPhoneNumber = userProfile.SecondaryPhoneNumber;

            //profile.Photo = userProfile.Photo;

            profile.ObjectState = ObjectState.Modified;
            _userProfileRepository.InsertOrUpdate(profile);
            _userProfileRepository.Save();

            return Request.CreateResponse(HttpStatusCode.OK, profile);
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
