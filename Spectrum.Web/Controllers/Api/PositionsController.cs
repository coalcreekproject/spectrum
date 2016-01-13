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
//using Microsoft.Practices.Unity;
using Spectrum.Web.Models;

namespace Spectrum.Web.Controllers.Api
{
    public class PositionsController : ApiController
    {
        private ICoreDbContext _context;
        private IPositionRepository _positionRepository;

        public PositionsController(ICoreUnitOfWork uow)
        {
            _context = uow.Context;

            //TODO: Newing this up is no good
            _positionRepository = new PositionRepository(uow);
        }

        //[Dependency]
        //public IRoleRepository RoleRepository
        //{
        //    get { return _positionRepository; }
        //    set { _positionRepository = value; }
        //}


        // GET: api/Positions
        [System.Web.Http.HttpGet]
        public IEnumerable<PositionViewModel> Get()
        {
            var positionViewModels = new List<PositionViewModel>();

            foreach (var p in _positionRepository.All)
            {
                positionViewModels.Add(Mapper.Map<PositionViewModel>(p));
            }

            //TODO: Get Paging working
            return positionViewModels;
        }

        [System.Web.Http.HttpGet]
        // GET: api/Positions/5
        public HttpResponseMessage Get(int id)
        {
            var positions = _positionRepository.All.Where(p => p.OrganizationId == id);
            var positionViewModels = new List<PositionViewModel>();

            foreach (var p in positions)
            {
                positionViewModels.Add(Mapper.Map<PositionViewModel>(p));
            }

            return Request.CreateResponse(HttpStatusCode.OK, positionViewModels);
        }

        // POST: api/Positions
        public async Task<HttpResponseMessage> Post([FromBody] PositionViewModel newPosition)
        {
            var position = Mapper.Map<Position>(newPosition);

            position.ObjectState = ObjectState.Added;
            _positionRepository.InsertOrUpdate(position);

            var result = Task.FromResult(_positionRepository.SaveAsync());

            if (result.IsCompleted)
            {
                return Request.CreateResponse(HttpStatusCode.Created,
                    position);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // PUT: api/Positions/5
        public HttpResponseMessage Put(int id, [FromBody] PositionViewModel editPosition)
        {
            var position = _positionRepository.FindAsync(editPosition.Id).Result;

            if (position == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            Mapper.Map(editPosition, position);

            position.ObjectState = ObjectState.Modified;

            _positionRepository.InsertOrUpdate(position);
            _positionRepository.Save();

            return Request.CreateResponse(HttpStatusCode.OK, position);
        }

        // DELETE: api/Positions/5
        public HttpResponseMessage Delete(int id)
        {
            var position = _positionRepository.FindAsync(id).Result;

            if (position == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            position.ObjectState = ObjectState.Deleted;
            _positionRepository.Delete(position.Id);
            _positionRepository.SaveAsync();

            return Request.CreateResponse(HttpStatusCode.OK, position);
        }
    }
}

