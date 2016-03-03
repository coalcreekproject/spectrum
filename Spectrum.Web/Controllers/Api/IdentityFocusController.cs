using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Spectrum.Data.Core.Models;
using Spectrum.Data.Core.Models.Interfaces;
using Spectrum.Logic.Identity;
using Spectrum.Web.Models;

namespace Spectrum.Web.Controllers.Api
{
    public class IdentityFocusController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var currentUser = UserUtility.GetUserFromMemoryCache(Convert.ToInt32(User.Identity.GetUserId()));

            if (currentUser == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<UserViewModel>(currentUser));
        }

        [HttpPut]
        public HttpResponseMessage Put([FromBody]UserViewModel userViewModel)
        {
            var currentUser = UserUtility.GetUserFromMemoryCache(userViewModel.Id);

            if (currentUser == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            currentUser.SelectedOrganizationId = userViewModel.SelectedOrganizationId;
            currentUser.SelectedOrganizationName =
                currentUser.UserOrganizations.FirstOrDefault(
                    o => o.OrganizationId.Equals(userViewModel.SelectedOrganizationId))?.Organization.Name;

            currentUser.SelectedRoleId = userViewModel.SelectedRoleId;
            currentUser.SelectedRoleName =
                currentUser.UserRoles.FirstOrDefault(r => r.RoleId.Equals(userViewModel.SelectedRoleId))?.Role.Name;

            currentUser.SelectedPositionId = userViewModel.SelectedPositionId;
            currentUser.SelectedPositionName =
                currentUser.UserPositions.FirstOrDefault(p => p.PositionId.Equals(userViewModel.SelectedPositionId))?.Position.Name;

            UserUtility.MemoryCacheUser(currentUser);

            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<UserViewModel>(currentUser));
        }
    }
}
