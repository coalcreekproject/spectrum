using System;
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
            _userRepository = new UserRepository(uow);
        }

        // GET: api/UserPositions/5
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Get(int id)
        {
            var user = _userRepository.FindByIdAsync(id).Result;
            var userPositions = user.UserPositions;

            var userPositionViewModels = userPositions.Select(r => Mapper.Map<PositionViewModel>(r.Position)).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, userPositionViewModels);
        }


        // PUT: api/Positions/5
        [System.Web.Http.HttpPut]
        public HttpResponseMessage Put([FromBody] UserViewModel editUser)
        {
            var user = _manager.FindById(editUser.Id);

            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            foreach (var r in user.UserPositions.ToList())
            {
                user.UserPositions.Remove(r);
                r.ObjectState = ObjectState.Deleted;
            }

            foreach (var r in editUser.UserPositions)
            {
                var tempUserPosition = new UserPosition();
                Mapper.Map(r, tempUserPosition);
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
