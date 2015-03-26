using System;
using Microsoft.ApplicationServer.Caching;
using Nucleus.JetBrains;

namespace CacheAspect
{
    [UsedImplicitly]
    public class OutOfProcessMemoryCache : ICache
    {
        private const String CacheName = "CacheAttribute";
        private static DataCache _cache;

        public OutOfProcessMemoryCache()
        {
            var configuration = new DataCacheFactoryConfiguration();
            var factory = new DataCacheFactory(configuration);
            _cache = factory.GetCache(CacheName);
        }

        public object this[string key]
        {
            get { return _cache[key]; }
            set { _cache[key] = value; }
        }


        public bool Contains(string key)
        {
            //App Fabric Cache has no Contains method
            return _cache[key] != null;
        }


        public void Delete(string key)
        {
            _cache.Remove(key);
        }

        public void Clear()
        {
            throw new NotImplementedException("Clearing AppFabric cache has not yet been implemented.");
        }
    }
}