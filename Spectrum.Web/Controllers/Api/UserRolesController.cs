using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
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

        public UserRolesController(ICoreUnitOfWork uow)
        {
            _context = uow.Context;

            //TODO: Newing this up is no good
            _userRepository = new UserRepository(uow);
        }

        [System.Web.Http.HttpGet]
        // GET: api/Roles/5
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

        // POST: api/Roles
        //public async Task<HttpResponseMessage> Post([FromBody] RoleViewModel newRoles)
        //{
        //    var role = Mapper.Map<Role>(newRole);

        //    role.ObjectState = ObjectState.Added;
        //    _roleRepository.InsertOrUpdate(role);

        //    var result = Task.FromResult(_roleRepository.SaveAsync());

        //    if (result.IsCompleted)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.Created,
        //            role);
        //    }

        //    return Request.CreateResponse(HttpStatusCode.BadRequest);
        //}

        //// PUT: api/Roles/5
        //public HttpResponseMessage Put(int id, [FromBody] RoleViewModel editRole)
        //{
        //    var role = _roleRepository.FindAsync(editRole.Id).Result;

        //    if (role == null)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.NotFound);
        //    }

        //    Mapper.Map(editRole, role);

        //    role.ObjectState = ObjectState.Modified;

        //    _roleRepository.InsertOrUpdate(role);
        //    _roleRepository.Save();

        //    return Request.CreateResponse(HttpStatusCode.OK, role);
        //}

        //// DELETE: api/Roles/5
        //public HttpResponseMessage Delete(int id)
        //{
        //    var role = _roleRepository.FindAsync(id).Result;

        //    if (role == null)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.NotFound);
        //    }

        //    role.ObjectState = ObjectState.Deleted;
        //    _roleRepository.Delete(role.Id);
        //    _roleRepository.SaveAsync();

        //    return Request.CreateResponse(HttpStatusCode.OK, role);
        //}

    }
}
