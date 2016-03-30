using System;
using System.Linq;
using System.Runtime.Caching;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Spectrum.Data.Core.Caching;
using Spectrum.Data.Core.Caching.Extensions;
using Spectrum.Data.Core.Context.UnitOfWork;
using Spectrum.Data.Core.Models;
using Spectrum.Data.Core.Repositories;
using Spectrum.Logic.Models;

namespace Spectrum.Logic.Identity
{
    public static class UserUtility
    {
        //TODO: Look at this mechanism, we want to inject a cache provider
        public static void RedisCacheUser(User user)
        {
            var userModel = Mapper.Map<UserModel>(user);
            var cache = RedisCache.Connection.GetDatabase();

            cache.Set("user:" + userModel.Id, userModel);
        }

        public static void MemoryCacheUser(User user)
        {
            var userModel = Mapper.Map<UserModel>(user);
            var cache = MemoryCache.Default;

            cache.Set("user:" + userModel.Id, userModel, DateTimeOffset.Now.AddMinutes(120));
        }

        public static void MemoryCacheUser(UserModel userModel)
        {
            var cache = MemoryCache.Default;
            cache.Set("user:" + userModel.Id, userModel, DateTimeOffset.Now.AddMinutes(120));
        }

        public static UserModel GetUserFromMemoryCache(IPrincipal user)
        {
            var id = user.Identity.GetUserId();

            var cache = MemoryCache.Default;
            var userModel = cache.Get<UserModel>("user:" + id);

            if (userModel == null) //re-cache
            {
                var userRepository = new UserRepository(new CoreUnitOfWork());
                var repoUser = userRepository.Users.FirstOrDefault(u => u.Id == (Convert.ToInt32(id)));
                MemoryCacheUser(repoUser);
                userModel = cache.Get<UserModel>("user:" + id);
            }

            return userModel;
        }

        public static UserModel GetUserFromMemoryCache(int userId)
        {
            var cache = MemoryCache.Default;
            var userModel = cache.Get<UserModel>("user:" + userId);

            return userModel;
        }

        private static Tuple<int, string> GetDefaultOrganizationData(User user)
        {
            var defaultOrganization = user.UserOrganizations.FirstOrDefault(o => o.Default == true);

            if (defaultOrganization != null)
            {
                return new Tuple<int, string>(defaultOrganization.OrganizationId, defaultOrganization.Organization.Name);
            }

            var firstOrganization = user.UserOrganizations.First();

            if (firstOrganization != null)
            {
                return new Tuple<int, string>(firstOrganization.OrganizationId, firstOrganization.Organization.Name);
            }

            return new Tuple<int, string>(0, "No Associated Organization"); //not found
        }

        private static Tuple<int, string> GetDefaultRoleData(User user)
        {
            var defaultRole = user.UserRoles.FirstOrDefault(o => o.Default == true);

            if (defaultRole != null)
            {
                return new Tuple<int, string>(defaultRole.RoleId, defaultRole.Role.Name);
            }

            var firstRole = user.UserRoles.FirstOrDefault();

            if (firstRole != null)
            {
                return new Tuple<int, string>(firstRole.RoleId, firstRole.Role.Name);
            }

            return new Tuple<int, string>(0, "No Roles Assigned"); //not found
        }

        private static Tuple<int, string> GetDefaultPositionData(User user)
        {
            var defaultPosition = user.UserPositions.FirstOrDefault(o => o.Default == true);

            if (defaultPosition != null)
            {
                return new Tuple<int, string>(defaultPosition.PositionId, defaultPosition.Position.Name);
            }

            var firstPosition = user.UserPositions.FirstOrDefault();

            if (firstPosition != null)
            {
                return new Tuple<int, string>(firstPosition.PositionId, firstPosition.Position.Name);
            }

            return new Tuple<int, string>(0, "No Positions Assigned"); //not found
        }

        public static void SetLoginIdentityFocus(User user)
        {
            var profile = user.UserProfiles.FirstOrDefault(p => p.Default == true);
            var userModel = Mapper.Map<UserModel>(user);

            var orgData = GetDefaultOrganizationData(user);
            //Set the default organization for the user
            userModel.SelectedOrganizationId = orgData.Item1;
            userModel.SelectedOrganizationName = orgData.Item2;

            //Set the default role for the user
            var roleData = GetDefaultRoleData(user);
            userModel.SelectedRoleId = roleData.Item1;
            userModel.SelectedRoleName = roleData.Item2;

            //Set the default position for the user
            var positionData = GetDefaultPositionData(user);
            userModel.SelectedPositionId = positionData.Item1;
            userModel.SelectedPositionName = positionData.Item2;

            MemoryCacheUser(userModel);
        }
    }
}