using System;
using System.Threading.Tasks;

namespace Spectrum.Data.Core.Caching.Interfaces
{
    public interface ICacheService
    {
        T GetFromCache<T>(string key, Func<T> missedCacheCall, TimeSpan timeToLive);
        T GetFromCache<T>(string key, Func<T> missedCacheCall);
        Task<T> GetFromCacheAsync<T>(string key, Func<Task<T>> missedCacheCall, TimeSpan timeToLive);
        Task<T> GetFromCacheAsync<T>(string key, Func<Task<T>> missedCacheCall);
        void InvalidateCache(string key);
    }
}