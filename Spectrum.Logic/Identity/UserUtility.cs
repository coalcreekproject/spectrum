using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Spectrum.Core.Data.Caching;
using Spectrum.Core.Data.Caching.Extensions;
using Spectrum.Logic.Models;

namespace Spectrum.Logic.Identity
{
    class UserUtility
    {
        public int GetLoggedInUserId()
        {
            //TODO: Get User out of cache.
            //var cache = RedisCache.Connection.GetDatabase();
            //var userModel = cache.StringGet("user:" + userModel.Id, userModel);


            return 1;
        }



    }
}