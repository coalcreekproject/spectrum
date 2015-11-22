﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
//using Microsoft.Practices.Unity;
using Spectrum.Core.Data.Context.Interfaces;
using Spectrum.Core.Data.Context.UnitOfWork;
using Spectrum.Core.Data.Models;
using Spectrum.Core.Data.Models.Interfaces;
using Spectrum.Core.Data.Repositories;
using Spectrum.Core.Data.Repositories.Interfaces;
using Spectrum.Web.Models;

namespace Spectrum.Web.Controllers.Api
{
    public class RolesController : ApiController
    {
        private ICoreDbContext _context;
        private IRoleRepository _roleRepository;

        public RolesController(ICoreUnitOfWork uow)
        {
            _context = uow.Context;

            //TODO: Newing this up is no good
            _roleRepository = new RoleRepository(uow);
        }

        //[Dependency]
        //public IRoleRepository RoleRepository
        //{
        //    get { return _roleRepository; }
        //    set { _roleRepository = value; }
        //}


        // GET: api/Roles
        [System.Web.Http.HttpGet]
        public IEnumerable<RoleViewModel> Get()
        {
            var roleViewModels = new List<RoleViewModel>();

            foreach (Role p in _roleRepository.All)
            {
                var roleViewModel = Mapper.Map<RoleViewModel>(p);
                roleViewModels.Add(roleViewModel);
            }

            //TODO: Get Paging working
            return roleViewModels;
        }

        [System.Web.Http.HttpGet]
        // GET: api/Roles/5
        public HttpResponseMessage Get(int id)
        {
            var role = _roleRepository.All.Where(r => r.OrganizationId == id);

            if (role == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            //var roleViewModel = Mapper.Map<RoleViewModel>(role);

            return Request.CreateResponse(HttpStatusCode.OK, role);
        }

        // POST: api/Roles
        public async Task<HttpResponseMessage> Post([FromBody] RoleViewModel newRole)
        {
            var role = Mapper.Map<Role>(newRole);

            role.ObjectState = ObjectState.Added;
            _roleRepository.InsertOrUpdate(role);

            var result = Task.FromResult(_roleRepository.SaveAsync());

            if (result.IsCompleted)
            {
                return Request.CreateResponse(HttpStatusCode.Created,
                    role);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // PUT: api/Roles/5
        public HttpResponseMessage Put(int id, [FromBody] RoleViewModel editRole)
        {
            var role = _roleRepository.FindAsync(editRole.Id).Result;

            if (role == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            Mapper.Map(editRole, role);

            role.ObjectState = ObjectState.Modified;

            _roleRepository.InsertOrUpdate(role);
            _roleRepository.Save();

            return Request.CreateResponse(HttpStatusCode.OK, role);
        }

        // DELETE: api/Roles/5
        public HttpResponseMessage Delete(int id)
        {
            var role = _roleRepository.FindAsync(id).Result;

            if (role == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            role.ObjectState = ObjectState.Deleted;
            _roleRepository.Delete(role.Id);
            _roleRepository.SaveAsync();

            return Request.CreateResponse(HttpStatusCode.OK, role);
        }
    }
}
