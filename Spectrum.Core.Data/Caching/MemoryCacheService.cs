using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using Spectrum.Data.Core.Caching.Interfaces;

namespace Spectrum.Data.Core.Caching
{
    class MemoryCacheService : ICacheService
    {
        //TODO: dependency injection

        private static MemoryCache _cache = new MemoryCache("SpectrumCache");

        public T GetFromCache<T>(string key, Func<T> missedCacheCall, TimeSpan timeToLive)
        {
            var obj = _cache.Get(key);

            if (obj == null)
            {
                obj = missedCacheCall();

                if (obj != null)
                {
                    obj = _cache.Get(key);
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
            //IDatabase cache = Connection.GetDatabase();
            //var obj = await cache.GetAsync<T>(key);
            //if (obj == null)
            //{
            //    obj = await missedCacheCall();
            //    if (obj != null)
            //    {
            //        cache.Set(key, obj);
            //    }
            //}
            //return obj;
        }

        public Task<T> GetFromCacheAsync<T>(string key, Func<Task<T>> missedCacheCall)
        {
            //return await GetFromCacheAsync<T>(key, missedCacheCall, TimeSpan.FromMinutes(5));
        }

        public void InvalidateCache(string key)
        {
            _cache.Remove(key);
        }
    }
}
