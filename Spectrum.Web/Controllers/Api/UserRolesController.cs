using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Spectrum.Data.Core.Context.Interfaces;
using Spectrum.Data.Core.Context.UnitOfWork;
using Spectrum.Data.Core.Models;
using Spectrum.Data.Core.Models.Interfaces;
using Spectrum.Data.Core.Repositories;
using Spectrum.Web.IdentityConfig;
using Spectrum.Web.Models;

namespace Spectrum.Web.Controllers.Api
{
    public class UserRolesController : ApiController
    {
        private ICoreDbContext _context;
        private UserRepository _userRepository;
        private readonly ApplicationUserManager _manager;

        public UserRolesController(ICoreUnitOfWork uow)
        {
            _context = uow.Context;

            _userRepository = new UserRepository(uow);
            _manager = new ApplicationUserManager(_userRepository);
        }

        // TODO: Get a mapper working for this.
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            var user = _userRepository.FindByIdAsync(id).Result;
            var userRoles = user.UserRoles;

            var userRoleViewModels = userRoles.Select(r => new UserRoleViewModel
            {
                ApplicationId = r.Role.ApplicationId,
                Default = r.Default,
                Description = r.Role.Description,
                Name = r.Role.Name,
                OrganizationId = r.OrganizationId,
                RoleId = r.RoleId,
                UserId = r.UserId
            }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, userRoleViewModels);
        }


        // PUT: api/Roles/5
        [HttpPut]
        public HttpResponseMessage Put([FromBody] UserViewModel editUser)
        {
            var user = _manager.FindById(editUser.Id);

            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            foreach (var r in user.UserRoles.ToList())
            {
                user.UserRoles.Remove(r);
                r.ObjectState = ObjectState.Deleted;
            }

            foreach (var r in editUser.UserRoles)
            {
                var tempUserRole = new UserRole();
                Mapper.Map(r, tempUserRole);
                user.UserRoles.Add(tempUserRole);
            }

            var result = _manager.Update(user);

            if (result.Succeeded)
            {
                return Request.CreateResponse(HttpStatusCode.Created, user);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
