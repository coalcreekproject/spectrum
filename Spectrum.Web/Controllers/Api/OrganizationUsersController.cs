using System;
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
    public class OrganizationUsersController : ApiController
    {
        private ICoreDbContext _context;
        private IUserRepository _userRepository;
        private IOrganizationUserRepository _organizationUserRepository;

        public OrganizationUsersController(ICoreUnitOfWork uow)
        {
            _context = uow.Context;

            //TODO: Newing this up is no good
            _organizationUserRepository = new OrganizationUserRepository(uow);
            _userRepository = new UserRepository(uow);
        }

        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            //var organization = _organizationRepository.Find(organizationId);
            var users = _userRepository.Users.ToList();

            List<UserViewModel> availableUsers = Mapper.Map<List<User>, List<UserViewModel>>(users);
            var assignedUserTemp = users.Where(u => u.UserOrganizations.Any(uo => uo.OrganizationId == id));
            List<UserViewModel> assignedUsers = Mapper.Map<List<User>, List<UserViewModel>>(assignedUserTemp.ToList());
            SortUsers(availableUsers, assignedUsers);

            return Request.CreateResponse(HttpStatusCode.OK,
                new Tuple<List<UserViewModel>, List<UserViewModel>>(availableUsers, assignedUsers));
        }

        private void SortUsers(List<UserViewModel> available, List<UserViewModel> assigned)
        {
            foreach (var u in available.ToList())
            {
                if (assigned.Any(x => x.Id == u.Id))
                {
                    available.Remove(u);
                }
            }
        }

        // PUT: api/OrganizationProfiles/5
        [HttpPut]
        public async Task<HttpResponseMessage> Put(int id, [FromBody]UserListViewModel lists)
        {
            var orgUsers = _organizationUserRepository.All.Where(uo => uo.OrganizationId.Equals(id));

            foreach (var u in lists.Assigned)
            {
                if (!orgUsers.Any(ou => ou.OrganizationId.Equals(id) && ou.UserId.Equals(u.Id)))
                {
                    _organizationUserRepository.InsertOrUpdate(new UserOrganization
                    {
                        Default = false,
                        ObjectState = ObjectState.Added,
                        OrganizationId = id,
                        UserId = u.Id
                    });
                }
            }

            foreach (var u in lists.Available)
            {
                _organizationUserRepository.Delete(id, u.Id);
            }

            await _organizationUserRepository.SaveAsync();

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
