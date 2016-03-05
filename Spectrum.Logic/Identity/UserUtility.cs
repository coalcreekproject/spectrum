﻿using System;
using System.Linq;
using System.Runtime.Caching;
using System.Runtime.CompilerServices;
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

            cache.Set("user:" + userModel.Id, userModel, DateTimeOffset.Now.AddMinutes(30));
        }

        public static void MemoryCacheUser(UserModel userModel)
        {
            var cache = MemoryCache.Default;
            cache.Set("user:" + userModel.Id, userModel, DateTimeOffset.Now.AddMinutes(30));
        }

        public static UserModel GetUserFromMemoryCache(IPrincipal user)
        {
            var id = user.Identity.GetUserId();

            var cache = MemoryCache.Default;
            var userModel = cache.Get<UserModel>("user:" + id);

            return userModel;
        }

        public static UserModel GetUserFromMemoryCache(int userId)
        {
            var cache = MemoryCache.Default;
            var userModel = cache.Get<UserModel>("user:" + userId);

            return userModel;
        }

        public static void SetLoginIdentityFocus(User user)
        {
            var profile = user.UserProfiles.FirstOrDefault(p => p.Default == true);
            var userModel = Mapper.Map<UserModel>(user);

            //Set the default organization for the user
            var defaultOrganization = user.UserOrganizations.FirstOrDefault(o => o.OrganizationId == profile?.OrganizationId && o.Default == true);

            if (defaultOrganization != null)
            {
                userModel.SelectedOrganizationId = defaultOrganization.OrganizationId;
                userModel.SelectedOrganizationName = defaultOrganization.Organization.Name;
            }
            else
            {
                var firstOrganization = user.UserOrganizations.FirstOrDefault(o => o.OrganizationId == profile?.OrganizationId);

                if (firstOrganization != null)
                {
                    userModel.SelectedOrganizationId = firstOrganization.OrganizationId;
                    userModel.SelectedOrganizationName = firstOrganization.Organization.Name;
                }
            }

            //TODO: Should always have some value, business rule is, every user has a default profile and an organization.
            if (defaultOrganization != null)
            {
                userModel.SelectedOrganizationId = defaultOrganization.OrganizationId;
            }

            //Set the default role for the user
            var defaultRole = user.UserRoles.FirstOrDefault(r =>
            {
                return r.Default != null && (r.OrganizationId == userModel.SelectedOrganizationId && r.Default == true);
            });

            if (defaultRole != null)
            {
                userModel.SelectedRoleId = defaultRole.RoleId;
                userModel.SelectedRoleName = defaultRole.Role.Name;
            }
            else
            {
                var firstRole = user.UserRoles.FirstOrDefault(r => r.OrganizationId == userModel.SelectedOrganizationId);

                if (firstRole != null)
                {
                    userModel.SelectedRoleId = firstRole.RoleId;
                    userModel.SelectedRoleName = firstRole.Role.Name;
                }
            }

            //Set the default position for the user.
            var defaultPosition = user.UserPositions.FirstOrDefault(p => p.OrganizationId == userModel.SelectedOrganizationId && p.Default == true);

            if (defaultPosition != null)
            {
                userModel.SelectedPositionId = defaultPosition.PositionId;
                userModel.SelectedPositionName = defaultPosition.Position.Name;
            }
            else
            {
                var firstPosition = user.UserPositions.FirstOrDefault(p => p.OrganizationId == userModel.SelectedOrganizationId);

                if (firstPosition != null)
                {
                    userModel.SelectedPositionId = firstPosition.PositionId;
                    userModel.SelectedPositionName = firstPosition.Position.Name;
                }
            }

            MemoryCacheUser(userModel);
        }

    }
}