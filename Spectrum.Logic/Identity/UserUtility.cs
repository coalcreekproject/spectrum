using System;
using System.Linq;
using System.Runtime.Caching;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using Spectrum.Data.Core.Caching;
using Spectrum.Data.Core.Caching.Extensions;
using Spectrum.Logic.Models;

namespace Spectrum.Logic.Identity
{
    public static class UserUtility
    {
        public static int GetLoggedInUserId(IPrincipal user)
        {
            var id = user.Identity.GetUserId();
            return Convert.ToInt32(id);
        }

        public static OrganizationModel GetLoggedInUserDefaultOrganization(IPrincipal user)
        {
            var userModel = GetUserFromCache(user);
            var defaultProfile = userModel.UserProfileModels.FirstOrDefault(p => p.Default == true);
            var organizationModel = userModel.OrganizationModels.FirstOrDefault(
                o => o.Id == defaultProfile.OrganizationId);
            return organizationModel;
        }

        public static UserModel GetUserFromCache(IPrincipal user)
        {
            var id = user.Identity.GetUserId();

            var cache = MemoryCache.Default;
            var userModel = cache.Get<UserModel>("user:" + id);

            return userModel;
        }
    }
}