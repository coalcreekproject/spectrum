using System;
using Spectrum.Core.Data.Caching.Extensions;
using StackExchange.Redis;

namespace Spectrum.Core.Data.Caching
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

        public void InvalidateCache(string key)
        {
            IDatabase cache = Connection.GetDatabase();
            cache.KeyDelete(key);
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect("spectrumoperational.redis.cache.windows.net, abortConnect = false, ssl = true, password = rXWM/RL/XC2vomxd/WyoxZRGDpkhRXwxKdYlOm9Os3g=");
        });

        public static ConnectionMultiplexer Connection
        {
            get { return lazyConnection.Value; }
        }
    }
}
