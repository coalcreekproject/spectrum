using Spectrum.Core.Data.Context.Extensions;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Utility.Utilities.Cache
{
    public class Cache
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
            return ConnectionMultiplexer.Connect("contoso5.redis.cache.windows.net, abortConnect = false, ssl = true, password =...");
        });

        public static ConnectionMultiplexer Connection
        {
            get { return lazyConnection.Value; }
        }
    }
}
