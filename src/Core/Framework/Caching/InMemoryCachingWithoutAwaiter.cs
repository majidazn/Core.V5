using Framework.Caching.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Framework.Caching
{
    public class InMemoryCachingWithoutAwaiter : IMemoryCachingServices
    {
        private IMemoryCache _memoryCache;
        private readonly ILogger<InMemoryCachingWithoutAwaiter> _logger;

        /// <summary>
        /// Impelementation : 
        ///      await cachingService.GetOrCreateAsync<model>(
        ///     dataKey, -- key
        ///    () => Task.FromResult(GetData()),   -- data
        ///    TimeSpan.FromSeconds(5)); -- expiration time
        /// </summary>
        /// <param name="memoryCache">use IMemoryCache as dependency</param>
        public InMemoryCachingWithoutAwaiter(ILogger<InMemoryCachingWithoutAwaiter> logger, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(_memoryCache));
            _logger = logger;
        }

        public   Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan memoryCacheExpiration) where T : class
        {
            if (typeof(T) == typeof(string))
            {
                throw new NotSupportedException("This method does not support 'string' type. Please use GetOrCreateStringAsync method instead.");
            }
            return   GetOrCreateAsync(new JsonConverter<T>(), key, factory, memoryCacheExpiration);
        }

        public   Task<string> GetOrCreateStringAsync(string key, Func<Task<string>> factory, TimeSpan memoryCacheExpiration)
        {
            return   GetOrCreateAsync(new StringConverter(), key, factory, memoryCacheExpiration);
        }

        public   Task<T> GetOrCreateAsync<T>(IConverter<T> converter, string key, Func<Task<T>> factory, TimeSpan memoryCacheExpiration)
        {
            return   _memoryCache.GetOrCreateAsync(key, entry =>
            {
                TimeSpan calculatedCacheExpiration = memoryCacheExpiration;

                entry.AbsoluteExpiration = DateTime.UtcNow.Add(memoryCacheExpiration);
                return GetFromCache(converter, key, factory, calculatedCacheExpiration);
            });
        }

        private async Task<T> GetFromCache<T>(IConverter<T> converter, string generatedKey, Func<Task<T>> factory, TimeSpan calculatedDistributedCacheExpiration)
        {
            //_logger.LogDebug("Getting cached value from Distributed cache for key {Key}", generatedKey);
            try
            {
                string val;
                var cachedItem = _memoryCache.TryGetValue(generatedKey, out val);
                if (cachedItem)
                {
                    //      _logger.LogDebug("Read cached value from In-Memory cache for key {Key}", generatedKey);
                    var value = converter.Deserialize(val);
                    return value;
                }
            }
            catch (Exception e)
            {
                // _logger.LogWarning(e, "Exception getting cached item from In-Memory cache.");
            }

            var item = await factory.Invoke();
            if (item != null)
            {
                try
                {
                    var serializedValue = converter.Serialize(item);
                    _memoryCache.Set<string>(generatedKey, serializedValue, calculatedDistributedCacheExpiration);
                       _logger.LogDebug("Stored in In-Memory cache for key {Key}", generatedKey);
                }
                catch (Exception e)
                {
                      _logger.LogWarning(e, "Exception storing cached item in  In-Memory cache.");
                }
            }

            return item;
        }

        public bool IsInCache(string key)
        {
            var cache = _memoryCache.Get(key);
            if (cache == null)
                return false;

            return true;
        }

        public void RemoveCache(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}
