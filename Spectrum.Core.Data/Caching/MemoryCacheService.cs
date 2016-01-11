using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using Spectrum.Data.Core.Caching.Interfaces;
using Spectrum.Data.Core.Caching.Extensions;

namespace Spectrum.Data.Core.Caching
{
    public class MemoryCacheService : ICacheService
    {
        //TODO: dependency injection

        //Create a custom Timeout of 10 seconds
        //CacheItemPolicy policy = new CacheItemPolicy();
        //policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(10.0);


        private static MemoryCache _cache = MemoryCache.Default;

        public T GetFromCache<T>(string key, Func<T> missedCacheCall, TimeSpan timeToLive)
        {
            var obj = _cache.Get<T>(key);

            if (obj == null)
            {
                obj = missedCacheCall();

                if (obj != null)
                {
                    _cache.Set(key, obj, DateTimeOffset.Now.AddMinutes(5));
                }
            }
            return obj;
        }

        public T GetFromCache<T>(string key, Func<T> missedCacheCall)
        {
            return GetFromCache<T>(key, missedCacheCall, TimeSpan.FromMinutes(5));
        }

        public Task<T> GetFromCacheAsync<T>(string key, Func<Task<T>> missedCacheCall, TimeSpan timeToLive)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetFromCacheAsync<T>(string key, Func<Task<T>> missedCacheCall)
        {
            throw new NotImplementedException();
        }

        public void InvalidateCache(string key)
        {
            _cache.Remove(key);
        }
    }
}
