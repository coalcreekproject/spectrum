using System;
using System.Threading.Tasks;
using Spectrum.Data.Core.Caching.Extensions;
using StackExchange.Redis;

namespace Spectrum.Data.Core.Caching
{
    public class RedisCache
    {
        public T GetFromCache<T>(string key, Func<T> missedCacheCall, TimeSpan timeToLive)
        {
            IDatabase cache = Connection.GetDatabase();
            var obj = cache.Get<T>(key);
            if (obj == null)
            {
                obj = missedCacheCall();
                if (obj != null)
                {
                    cache.Set(key, obj);
                }
            }
            return obj;
        }
        public T GetFromCache<T>(string key, Func<T> missedCacheCall)
        {
            return GetFromCache<T>(key, missedCacheCall, TimeSpan.FromMinutes(5));
        }

        public async Task<T> GetFromCacheAsync<T>(string key, Func<Task<T>> missedCacheCall, TimeSpan timeToLive)
        {
            IDatabase cache = Connection.GetDatabase();
            var obj = await cache.GetAsync<T>(key);
            if (obj == null)
            {
                obj = await missedCacheCall();
                if (obj != null)
                {
                    cache.Set(key, obj);
                }
            }
            return obj;
        }
        public async Task<T> GetFromCacheAsync<T>(string key, Func<Task<T>> missedCacheCall)
        {
            return await GetFromCacheAsync<T>(key, missedCacheCall, TimeSpan.FromMinutes(5));
        }

        public void InvalidateCache(string key)
        {
            IDatabase cache = Connection.GetDatabase();
            cache.KeyDelete(key);
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            ConfigurationOptions options = new ConfigurationOptions();
            options.EndPoints.Add("spectrumoperational.redis.cache.windows.net");
            options.Ssl = true;
            options.Password = "rXWM/RL/XC2vomxd/WyoxZRGDpkhRXwxKdYlOm9Os3g=";
            options.ConnectTimeout = 1000;
            options.SyncTimeout = 2500;
            return ConnectionMultiplexer.Connect(options);
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}