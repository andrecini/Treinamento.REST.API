using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Treinamento.REST.Domain.Entities;

namespace Treinamento.REST.Services.Services
{
    public static class CacheHelper
    {
        public static T GetOrSet<T>(IMemoryCache cache, string key, Func<T> getItemCallback, TimeSpan absoluteExpiration)
        {
            if (!cache.TryGetValue(key, out T item))
            {
                item = getItemCallback();

                if (item != null)
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = absoluteExpiration
                    };

                    cache.Set(key, item, cacheEntryOptions);
                }
            }

            return item;
        }

        public static void Remove(IMemoryCache cache, string key)
        {
            cache.Remove(key);
        }
    }
}

