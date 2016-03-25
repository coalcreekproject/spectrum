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
using Spectrum.Data.Core.Repositories.Interfaces;
using Spectrum.Web.Models;
//using Microsoft.Practices.Unity;

namespace Spectrum.Web.Controllers.Api
{
    public class RolesController : ApiController
    {
        private ICoreUnitOfWork _uow;
        private ICoreDbContext _context;
        private IRoleRepository _roleRepository;

        public RolesController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
            _context = roleRepository.Context;
        }

        // GET: api/Roles
        [HttpGet]
        public IEnumerable<RoleViewModel> Get()
        {
            var roleViewModels = new List<RoleViewModel>();

            foreach (var r in _roleRepository.All)
            {
                roleViewModels.Add(Mapper.Map<RoleViewModel>(r));
            }

            //TODO: Get Paging working
            return roleViewModels;
        }

        [HttpGet]
        // GET: api/Roles/5
        public HttpResponseMessage Get(int id)
        {
            var roles = _roleRepository.All.Where(r => r.OrganizationId == id);
            var roleViewModels = new List<RoleViewModel>();

            foreach (var r in roles)
            {
                roleViewModels.Add(Mapper.Map<RoleViewModel>(r));
            }

            return Request.CreateResponse(HttpStatusCode.OK, roleViewModels);
        }

        [HttpGet]
        // GET: api/Roles/5
        public HttpResponseMessage Get(int roleId, int organizationId)
        {
            var role = _roleRepository.Find(roleId);

            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<RoleViewModel>(role));
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
                    Mapper.Map<RoleViewModel>(role));
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // PUT: api/Roles/5
        public HttpResponseMessage Put([FromBody] RoleViewModel editRole)
        {
            var role = _roleRepository.FindAsync(editRole.RoleId).Result;

            if (role == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            Mapper.Map(editRole, role);

            role.ObjectState = ObjectState.Modified;

            _roleRepository.InsertOrUpdate(role);
            _roleRepository.Save();

            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<RoleViewModel>(role));
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

            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<RoleViewModel>(role));
        }
    }
}

