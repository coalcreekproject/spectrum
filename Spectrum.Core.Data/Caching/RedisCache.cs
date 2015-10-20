using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Spectrum.Core.Data.Caching.Extensions;
using StackExchange.Redis;

namespace Spectrum.Core.Data.Caching
{
    public static class RedisCache
    {
        static readonly IDatabase Cache = Connection.GetDatabase();

        public static ConnectionMultiplexer Connection
        {
            //get { return lazyConnection.Value; }
            get { return ConnectionMultiplexer.Connect("spectrumoperational.redis.cache.windows.net, abortConnect = false, ssl = true, password = rXWM/RL/XC2vomxd/WyoxZRGDpkhRXwxKdYlOm9Os3g="); }
        }

        //private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        //{
        //    return ConnectionMultiplexer.Connect("spectrumoperational.redis.cache.windows.net, abortConnect = false, ssl = true, password = rXWM/RL/XC2vomxd/WyoxZRGDpkhRXwxKdYlOm9Os3g=");
        //});


        public static T GetFromCache<T>(string key, Func<T> missedCacheCall, TimeSpan timeToLive)
        {

            var obj = Cache.Get<T>(key);

            if (obj == null)
            {
                obj = missedCacheCall();

                if (obj != null)
                {
                    Cache.Set(key, obj);
                }
            }

            return obj;
        }

        public static T GetFromCache<T>(string key, Func<T> missedCacheCall)
        {
            return GetFromCache<T>(key, missedCacheCall, TimeSpan.FromMinutes(5));
        }

        public static void InvalidateCache(string key)
        {
            //IDatabase cache = Connection.GetDatabase();
            //cache.KeyDelete(key);
            Cache.KeyDelete(key);
        }

        //"Extension Methods"
        public static T Get<T>(string key)
        {
            return Deserialize<T>(Cache.StringGet(key));
        }

        public static object Get(string key)
        {
            return Deserialize<object>(Cache.StringGet(key));
        }

        public static void Set(string key, object value)
        {
            Cache.StringSet(key, Serialize(value));
        }


        //Serializers
        static byte[] Serialize(object o)
        {
            if (o == null)
            {
                return null;
            }

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, o);
                byte[] objectDataAsStream = memoryStream.ToArray();
                return objectDataAsStream;
            }
        }

        static T Deserialize<T>(byte[] stream)
        {
            if (stream == null)
            {
                return default(T);
            }

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream(stream))
            {
                T result = (T)binaryFormatter.Deserialize(memoryStream);
                return result;
            }
        }
    }
}
