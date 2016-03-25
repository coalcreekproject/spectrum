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
//using Microsoft.Practices.Unity;

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

        // GET: api/Positions
        [HttpGet]
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">This is the organization id</param>
        /// <returns></returns>
        [HttpGet]
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

        [HttpPost]
        // POST: api/Positions
        public async Task<HttpResponseMessage> Post([FromBody] PositionViewModel newPosition)
        {
            var position = Mapper.Map<Position>(newPosition);

            position.ObjectState = ObjectState.Added;
            _positionRepository.InsertOrUpdate(position);
            await _positionRepository.SaveAsync();

            return Request.CreateResponse(HttpStatusCode.Created,
                Mapper.Map<PositionViewModel>(position));
        }

        [HttpPut]
        // PUT: api/Positions/5
        public HttpResponseMessage Put(int id, [FromBody] PositionViewModel editPosition)
        {
            var position = _positionRepository.FindAsync(editPosition.PositionId).Result;

            if (position == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            Mapper.Map(editPosition, position);

            position.ObjectState = ObjectState.Modified;

            _positionRepository.InsertOrUpdate(position);
            _positionRepository.Save();

            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<PositionViewModel>(position));
        }

        [HttpDelete]
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

            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<PositionViewModel>(position));
        }
    }
}

