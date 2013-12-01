using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Provider
{
	public class Cache
	{
		public string Name {get;private set;}

		public Cache(string name) {
			Name = name;
		}

		public T GetCache<T>(string key)
		{
			if(string.IsNullOrEmpty(key)) return default(T);
			return (T)MemoryCache.Default[Name + key];
		}
		public void SetCache(string key, object obj)
		{
			MemoryCache.Default[Name + key] = obj;
		}

		public bool HasCache(string key)
		{
			return MemoryCache.Default[Name + key] != null;
		}

		public void ClearCache(string key)
		{
			MemoryCache.Default.Remove(Name + key);
		}
	}
}
