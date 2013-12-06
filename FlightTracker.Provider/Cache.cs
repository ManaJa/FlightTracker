using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Provider
{
	public class Cache : FlightTracker.Provider.ICache
	{
        private ObjectCache _Cache = null;
        public Cache() {
            _Cache = MemoryCache.Default;
        }

		public string Name {get; set;}
        public bool Disable { get; set; }

		public T GetCache<T>(string key)
		{
            if (Disable) return default(T);

			if(string.IsNullOrEmpty(key)) return default(T);
            return (T)_Cache[Name + key];
		}
		public void SetCache(string key, object obj)
		{
            if (Disable) return;

            _Cache[Name + key] = obj;
		}

		public bool HasCache(string key)
		{
            if (Disable) return false;

            return _Cache[Name + key] != null;
		}

		public void ClearCache(string key)
		{
            if (Disable) return;

            _Cache.Remove(Name + key);
		}
	}
}
