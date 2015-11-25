using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Spectrum.Core.Data.Caching;
using Spectrum.Core.Data.Caching.Extensions;
using Spectrum.Core.Data.Models;
using Spectrum.Logic.Models;
using StackExchange.Redis;

namespace Spectrum.Logic.Identity
{
    public static class UserUtility
    {
        public static int GetLoggedInUserId(IPrincipal user)
        {
            var id = user.Identity.GetUserId();
            return Convert.ToInt32(id);
        }

        public static UserModel GetUserFromCache(IPrincipal user)
        {
            var id = user.Identity.GetUserId();

            var cache = RedisCache.Connection.GetDatabase();
            var userModel = cache.Get<UserModel>("user:" + id);

            return userModel;
        }
    }
}