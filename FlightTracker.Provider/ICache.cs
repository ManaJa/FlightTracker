using System;
namespace FlightTracker.Provider
{
    public interface ICache
    {
        void ClearCache(string key);
        bool Disable { get; set; }
        T GetCache<T>(string key);
        bool HasCache(string key);
        string Name { get; set; }
        void SetCache(string key, object obj);
    }
}
