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
    public class RolesController : ApiController
    {
        private ICoreDbContext _context;
        private IRoleRepository _RoleRepository;

        public RolesController(ICoreUnitOfWork uow)
        {
            _context = uow.Context;

            //TODO: Newing this up is no good
            _RoleRepository = new RoleRepository(uow);
        }

        // GET: api/Roles
        [System.Web.Http.HttpGet]
        public IEnumerable<RoleViewModel> Get()
        {
            var RoleViewModels = new List<RoleViewModel>();

            foreach (Role p in _RoleRepository.All)
            {
                var RoleViewModel = Mapper.Map<RoleViewModel>(p);
                RoleViewModels.Add(RoleViewModel);
            }

            //TODO: Get Paging working
            return RoleViewModels;
        }

        [System.Web.Http.HttpGet]
        // GET: api/Roles/5
        public HttpResponseMessage Get(int id)
        {
            var Role = _RoleRepository.All.Where(r => r.Id == id);

            if (Role == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            //var RoleViewModel = Mapper.Map<RoleViewModel>(Role);

            return Request.CreateResponse(HttpStatusCode.OK, Role);
        }

        // POST: api/Roles
        public async Task<HttpResponseMessage> Post([FromBody]RoleViewModel newRole)
        {
            var Role = Mapper.Map<Role>(newRole);

            Role.ObjectState = ObjectState.Added;
            _RoleRepository.InsertOrUpdate(Role);
            var result = Task.FromResult(_RoleRepository.SaveAsync());

            if (result.IsCompleted)
            {
                return Request.CreateResponse(HttpStatusCode.Created,
                    Role);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // PUT: api/Roles/5
        public HttpResponseMessage Put(int id, [FromBody]RoleViewModel editRole)
        {
            var Role = _RoleRepository.FindAsync(editRole.Id).Result;

            if (Role == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            Mapper.Map(editRole, Role);

            //Role.Default = editRole.Default;
            //Role.ProfileName = editRole.ProfileName;
            //Role.Description = editRole.Description;
            //Role.PrimaryContact = editRole.PrimaryContact;
            //Role.Phone = editRole.Phone;
            //Role.Fax = editRole.Fax;
            //Role.Email = editRole.Email;
            //Role.Country = editRole.Country;
            //Role.County = editRole.County;
            //Role.TimeZone = editRole.TimeZone;
            //Role.Language = editRole.Language;
            //Role.Notes = editRole.Notes;


            Role.ObjectState = ObjectState.Modified;
            //Role.Organization.ObjectState = ObjectState.Unchanged;

            _RoleRepository.InsertOrUpdate(Role);
            _RoleRepository.Save();

            return Request.CreateResponse(HttpStatusCode.OK, Role);
        }

        // DELETE: api/Roles/5
        public HttpResponseMessage Delete(int id)
        {
            var Role = _RoleRepository.FindAsync(id).Result;

            if (Role == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            Role.ObjectState = ObjectState.Deleted;
            _RoleRepository.Delete(Role.Id);
            _RoleRepository.SaveAsync();

            return Request.CreateResponse(HttpStatusCode.OK, Role);
        }
    }
}

