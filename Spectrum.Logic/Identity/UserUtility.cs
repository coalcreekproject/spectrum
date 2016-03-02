using System;
using System.Linq;
using System.Runtime.Caching;
using System.Security.Principal;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Spectrum.Data.Core.Caching;
using Spectrum.Data.Core.Caching.Extensions;
using Spectrum.Data.Core.Models;
using Spectrum.Logic.Models;

namespace Spectrum.Logic.Identity
{
    public static class UserUtility
    {
        //TODO: Look at this mechanism, we want to inject a cache provider
        public static void RedisCacheLoggedInUser(User user)
        {
            var userModel = Mapper.Map<UserModel>(user);

            SetLoginIdentityFocus(user, userModel);

            var cache = RedisCache.Connection.GetDatabase();
            cache.Set("user:" + userModel.Id, userModel);
        }

        public static void MemoryCacheLoggedInUser(User user)
        {
            var userModel = Mapper.Map<UserModel>(user);

            SetLoginIdentityFocus(user, userModel);

            var cache = MemoryCache.Default;
            cache.Set("user:" + userModel.Id, userModel, DateTimeOffset.Now.AddMinutes(30));
        }

        private static void SetLoginIdentityFocus(User user, UserModel userModel)
        {
            var profile = user.UserProfiles.FirstOrDefault(p => p.Default == true);

            if (profile != null && profile.OrganizationId != null)
            {
                var orgId = Convert.ToInt32(profile.OrganizationId);
                var defaultUserOrg = user.UserOrganizations.FirstOrDefault(o => o.OrganizationId == orgId);
                if (defaultUserOrg != null)
                {
                    // Set the default organization for the user
                    userModel.SelectedOrganizationId = defaultUserOrg.OrganizationId;

                    // Set the default role for the user
                    userModel.SelectedRoleId =
                        user.UserRoles.FirstOrDefault(
                            r => r.OrganizationId == userModel.SelectedOrganizationId).RoleId;

                    var defaultUserPosition = user.UserPositions
                        .FirstOrDefault(p => p.OrganizationId == userModel.SelectedOrganizationId);

                    if (defaultUserPosition != null)
                    {
                        // Set the default position for the user.
                        userModel.SelectedPositionId = defaultUserPosition.PositionId;
                    }
                    userModel.SelectedPositionId = 1;
                }
            }
        }

        public static UserModel GetUserModelFromCache(IPrincipal user)
        {
            var id = user.Identity.GetUserId();

            var cache = MemoryCache.Default;
            var userModel = cache.Get<UserModel>("user:" + id);

            return userModel;
        }
    }
}