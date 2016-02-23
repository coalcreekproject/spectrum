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
using Spectrum.Web.Models;

namespace Spectrum.Web.Controllers.Api
{
    public class UserPositionsController : ApiController
    {
        private ICoreDbContext _context;
        private UserRepository _userRepository;
        private readonly UserManager<User, int> _manager;

        public UserPositionsController(ICoreUnitOfWork uow)
        {
            _context = uow.Context;

            //Ugh still newing stuff up...
            _userRepository = new UserRepository(uow);
            _manager = new UserManager<User, int>(_userRepository);
        }

        // GET: api/UserPositions/5
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            var user = _userRepository.FindByIdAsync(id).Result;
            var userPositions = user.UserPositions;

            var userPositionViewModels = userPositions.Select(u => new UserPositionViewModel
            {
                Default = u.Default,
                Description = u.Position.Description,
                Name = u.Position.Name,
                OrganizationId = u.OrganizationId,
                PositionId = u.PositionId,
                UserId = u.UserId,
                Value = u.Position.Value
            }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, userPositionViewModels);
        }


        // PUT: api/Positions/5
        [HttpPut]
        public HttpResponseMessage Put([FromBody] UserViewModel editUser)
        {
            var user = _manager.FindById(editUser.Id);

            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            foreach (var p in user.UserPositions.ToList())
            {
                user.UserPositions.Remove(p);
                p.ObjectState = ObjectState.Deleted;
            }

            foreach (var p in editUser.UserPositions)
            {
                var tempUserPosition = new UserPosition();
                Mapper.Map(p, tempUserPosition);
                user.UserPositions.Add(tempUserPosition);
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
