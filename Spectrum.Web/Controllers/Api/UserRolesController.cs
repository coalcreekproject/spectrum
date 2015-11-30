using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Spectrum.Core.Data.Context.Interfaces;
using Spectrum.Core.Data.Context.UnitOfWork;
using Spectrum.Core.Data.Models;
using Spectrum.Core.Data.Models.Interfaces;
using Spectrum.Core.Data.Repositories;
using Spectrum.Core.Data.Repositories.Interfaces;
using Spectrum.Web.Models;

namespace Spectrum.Web.Controllers.Api
{
    public class UserRolesController : ApiController
    {
        private ICoreDbContext _context;
        private UserRepository _userRepository;
        private readonly UserManager<User, int> _manager;

        public UserRolesController(ICoreUnitOfWork uow)
        {
            _context = uow.Context;

            _userRepository = new UserRepository(uow);
            _manager = new UserManager<User, int>(_userRepository);
        }

        // GET: api/Roles/5
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Get(int id)
        {
            var user = _userRepository.FindByIdAsync(id).Result;
            var roles = user.Roles;
            var userRoleViewModels = new List<RoleViewModel>();

            foreach (var r in roles)
            {
                userRoleViewModels.Add(Mapper.Map<RoleViewModel>(r));
            }

            return Request.CreateResponse(HttpStatusCode.OK, userRoleViewModels);
        }


        // PUT: api/Roles/5
        [System.Web.Http.HttpPut]
        public HttpResponseMessage Put([FromBody] UserViewModel editUser)
        {
            var user = _manager.FindById(editUser.Id);

            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            user.Roles.Clear();

            foreach (var r in editUser.Roles)
            {
                Role tempRole = new Role();
                Mapper.Map(r, tempRole);
                tempRole.ObjectState = ObjectState.Unchanged;
                user.Roles.Add(tempRole);
            }
            
            user.ObjectState = ObjectState.Modified;

            var result = _manager.Update(user);

            if (result.Succeeded)
            {
                return Request.CreateResponse(HttpStatusCode.Created, user);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
