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
        private IOrganizationRepository _organizationRepository;
        private IUserRepository _userRepository;

        public OrganizationUsersController(ICoreUnitOfWork uow)
        {
            _context = uow.Context;

            //TODO: Newing this up is no good
            _organizationRepository = new OrganizationRepository(uow);
            _userRepository = new UserRepository(uow);
        }

        // GET: api/Users/orgId
        //[HttpGet]
        //public IEnumerable<UserViewModel> Get(int organizationId)
        //{
        //    var userViewModels = new List<UserViewModel>();

        //    foreach (var u in _userRepository.Users)
        //    {
        //        userViewModels.Add(Mapper.Map<UserViewModel>(u));
        //    }

        //    //TODO: Get Paging working
        //    return userViewModels;
        //}

        //[HttpGet]
        //public HttpResponseMessage Get()
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK);
        //}

        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            //var organization = _organizationRepository.Find(organizationId);
            var users = _userRepository.Users.ToList();

            List<UserViewModel> availableUsers = Mapper.Map<List<User>, List<UserViewModel>>(users);
            var assignedUserTemp = users.Where(u => u.UserOrganizations.Any(uo => uo.OrganizationId == id));
            List<UserViewModel> assignedUsers = Mapper.Map<List<User>, List<UserViewModel>>(assignedUserTemp.ToList());
            SortRoles(availableUsers, assignedUsers);

            return Request.CreateResponse(HttpStatusCode.OK,
                new Tuple<List<UserViewModel>, List<UserViewModel>>(availableUsers, assignedUsers));
        }

        private void SortRoles(List<UserViewModel> available, List<UserViewModel> assigned)
        {
            foreach (var u in available.ToList())
            {
                if (assigned.Any(x => x.Id == u.Id))
                {
                    available.Remove(u);
                }
            }
        }

        private int GetDefaultOrganizationId(User user)
        {
            var defaultOrganization = user.UserOrganizations.FirstOrDefault(o => o.Default == true);

            if (defaultOrganization != null)
            {
                return defaultOrganization.OrganizationId;
            }

            var firstOrganization = user.UserOrganizations.First();

            if (firstOrganization != null)
            {
                return firstOrganization.OrganizationId;
            }

            return -1; //not found
        }


        // PUT: api/OrganizationProfiles/5
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody]UserListViewModel lists)
        {

            //List<User> Available = Mapper.Map<List<UserViewModel>, List<User>>(lists.Available);
            //List<User> Assigned = Mapper.Map<List<UserViewModel>, List<User>>(lists.Assigned);

            //foreach (var u in Assigned)
            //{
            //    u.UserOrganizations.Clear();
            //    u.UserOrganizations.Add(new UserOrganization
            //    {
            //        Default = false,
            //        ObjectState = ObjectState.Added,
            //        OrganizationId = id,
            //        UserId = u.Id
            //    });

            //    // TODO: This sucks dokey b@lls, add the standard suite of methods to this repo in addition to the retarded stuff MS wants for identity framework
            //    await _userRepository.UpdateAsync(u);
            //}

            //return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<User>, List<UserViewModel>>(users));
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
