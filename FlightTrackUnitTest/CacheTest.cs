using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace FlightTrackUnitTest
{
    [TestClass]
    public class CacheTest
    {

        [TestMethod]
        public void T11_CacheAdd()
        {
            var cache = Substitute.For<FlightTracker.Provider.Cache>();
            cache.Name = "CacheTest";

            var key = "CACHE_KEY";
            string val = "HELLO";

            cache.SetCache(key, val);
            Assert.AreEqual(cache.GetCache<string>(key), val);
            cache.ClearCache(key);
        }
        [TestMethod]
        public void T12_CacheClear()
        {
            var cache = Substitute.For<FlightTracker.Provider.Cache>();
            cache.Name = "CacheTest";

            var key = "CACHE_KEY";
            string val = "HELLO";

            cache.SetCache(key, val);
            Assert.AreEqual(cache.GetCache<string>(key), val);
            cache.ClearCache(key);
            Assert.IsNull(cache.GetCache<string>(key));
        }
        [TestMethod]
        public void T13_CacheDisable()
        {
            var cache = Substitute.For<FlightTracker.Provider.Cache>();
            cache.Name = "CacheTest";
            cache.Disable = true;

            var key = "CACHE_KEY";
            string val = "HELLO";

            cache.SetCache(key, val);
            Assert.IsNull(cache.GetCache<string>(key));
            Assert.IsFalse(cache.HasCache(key));

            cache.ClearCache(key);
            Assert.IsNull(cache.GetCache<string>(key));
        }

    }
}
