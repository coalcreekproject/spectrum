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
using System;
using System.Collections.Generic;

namespace Spectrum.Web.Controllers.Api
{
    public class UserPositionsController : ApiController
    {
        private ICoreDbContext _context;
        private UserRepository _userRepository;
        private PositionRepository _positionRepository;
        private readonly ApplicationUserManager _manager;

        public UserPositionsController(ICoreUnitOfWork uow)
        {
            _context = uow.Context;

            //Ugh still newing stuff up...
            _userRepository = new UserRepository(uow);
            _positionRepository = new PositionRepository(uow);
            _manager = new ApplicationUserManager(_userRepository);
        }

        // GET: api/UserPositions/5
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            var user = _userRepository.FindByIdAsync(id).Result;
            var organizationId = GetDefaultOrganizationId(user);
            var positions = _positionRepository.All.Where(r => r.OrganizationId == organizationId).ToList();

            var availableUserPositions = positions.Select(p => new UserPositionViewModel
            {
                Default = null,
                Description = p.Description,
                Name = p.Name,
                OrganizationId = organizationId,
                PositionId = p.Id,
                UserId = user.Id
            }).ToList();

            var assignedUserPositions = user.UserPositions.Select(r => new UserPositionViewModel
            {
                Default = r.Default,
                Description = r.Position.Description,
                Name = r.Position.Name,
                OrganizationId = r.OrganizationId,
                PositionId = r.PositionId,
                UserId = r.UserId
            }).ToList();

            SortPositions(availableUserPositions, assignedUserPositions);

            return Request.CreateResponse(HttpStatusCode.OK,
                new Tuple<List<UserPositionViewModel>, List<UserPositionViewModel>>(availableUserPositions, assignedUserPositions));
        }

        private void SortPositions(List<UserPositionViewModel> available, List<UserPositionViewModel> assigned)
        {
            foreach (var r in available.ToList())
            {
                if (assigned.Any(u => u.PositionId == r.PositionId))
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
