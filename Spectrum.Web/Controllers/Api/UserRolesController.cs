using System;
using System.Collections;
using System.Collections.Generic;
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
        private RoleRepository _roleRepository;
        private readonly ApplicationUserManager _manager;

        public UserRolesController(ICoreUnitOfWork uow)
        {
            _context = uow.Context;
            _userRepository = new UserRepository(uow);
            _roleRepository = new RoleRepository(uow);
            _manager = new ApplicationUserManager(_userRepository);
        }

        // TODO: Get a mapper working for this.
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            var user = _userRepository.FindByIdAsync(id).Result;
            var organizationId = GetDefaultOrganizationId(user);
            var roles = _roleRepository.All.Where(r => r.OrganizationId == organizationId).ToList();

            var availableUserRoles = roles.Select(r => new UserRoleViewModel
            {
                ApplicationId = r.ApplicationId,
                Default = null,
                Description = r.Description,
                Name = r.Name,
                OrganizationId = organizationId,
                RoleId = r.Id,
                UserId = user.Id
            }).ToList();

            var assignedUserRoles = user.UserRoles.Select(r => new UserRoleViewModel
            {
                ApplicationId = r.Role.ApplicationId,
                Default = r.Default,
                Description = r.Role.Description,
                Name = r.Role.Name,
                OrganizationId = r.OrganizationId,
                RoleId = r.RoleId,
                UserId = r.UserId
            }).ToList();


            SortRoles(availableUserRoles, assignedUserRoles);

            return Request.CreateResponse(HttpStatusCode.OK, 
                new Tuple<List<UserRoleViewModel>, List<UserRoleViewModel>>(availableUserRoles, assignedUserRoles));
        }

        private void SortRoles(List<UserRoleViewModel> available, List<UserRoleViewModel> assigned)
        {
            foreach (var r in available.ToList())
            {
                if (assigned.Any(u => u.RoleId == r.RoleId))
                {
                    available.Remove(r);
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
