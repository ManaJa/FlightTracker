using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using FlightTracker.Provider.Services;
using FlightTracker.Provider.Entity;

namespace FlightTrackUnitTest
{
    [TestClass]
    public class AirportInfoProviderTest
    {
        [TestMethod]
        public void T31_GetAirportInfo()
        {
            var testKey = "BNE";
            var testVal = "<NewDataSet> <Table> <AirportCode>BNE</AirportCode> <CityOrAirportName>BRISBANE INTL</CityOrAirportName> <Country>Australia</Country> <CountryAbbrviation>AU</CountryAbbrviation> <CountryCode>802</CountryCode> <GMTOffset>-10</GMTOffset> <RunwayLengthFeet>11483</RunwayLengthFeet> <RunwayElevationFeet>13</RunwayElevationFeet> <LatitudeDegree>27</LatitudeDegree> <LatitudeMinute>28</LatitudeMinute> <LatitudeSecond>0</LatitudeSecond> <LatitudeNpeerS>S</LatitudeNpeerS> <LongitudeDegree>153</LongitudeDegree> <LongitudeMinute>2</LongitudeMinute> <LongitudeSeconds>0</LongitudeSeconds> <LongitudeEperW>E</LongitudeEperW> </Table> <Table> <AirportCode>BNE</AirportCode> <CityOrAirportName>BRISBANE INTL</CityOrAirportName> <Country>Australia</Country> <CountryAbbrviation>AU</CountryAbbrviation> <CountryCode>802</CountryCode> <GMTOffset>-10</GMTOffset> <RunwayLengthFeet>11483</RunwayLengthFeet> <RunwayElevationFeet>13</RunwayElevationFeet> <LatitudeDegree>27</LatitudeDegree> <LatitudeMinute>28</LatitudeMinute> <LatitudeSecond>0</LatitudeSecond> <LatitudeNpeerS>S</LatitudeNpeerS> <LongitudeDegree>153</LongitudeDegree> <LongitudeMinute>2</LongitudeMinute> <LongitudeSeconds>0</LongitudeSeconds> <LongitudeEperW>E</LongitudeEperW> </Table> </NewDataSet>";
            var testKeyCache = "ABC";
            var testValEntity = new AirportInfoEntity { AirportCode = "ABC" };

            var service = Substitute.For<IAirportInfoService>();
            service.GetAirportInformationByAirportCode(testKey).Returns(testVal);
            
            var cache = Substitute.For<FlightTracker.Provider.ICache>();
            cache.GetCache<AirportInfoEntity>(testKeyCache).Returns(testValEntity);

            //from service
            var provider = new FlightTracker.Provider.AirportInfoProvider(service, cache);
            Assert.IsNotNull(provider.GetAirportInfo(testKey));
            Assert.AreEqual(testKey, provider.GetAirportInfo(testKey).AirportCode);

            //from cache
            Assert.AreEqual(testValEntity, provider.GetAirportInfo(testKeyCache));

            //no hit
            Assert.IsNull(provider.GetAirportInfo(null));
        }

        public void T32_GetAirportInfoAsync()
        {
            var testKey = "BNE";
            var testVal = "<NewDataSet> <Table> <AirportCode>BNE</AirportCode> <CityOrAirportName>BRISBANE INTL</CityOrAirportName> <Country>Australia</Country> <CountryAbbrviation>AU</CountryAbbrviation> <CountryCode>802</CountryCode> <GMTOffset>-10</GMTOffset> <RunwayLengthFeet>11483</RunwayLengthFeet> <RunwayElevationFeet>13</RunwayElevationFeet> <LatitudeDegree>27</LatitudeDegree> <LatitudeMinute>28</LatitudeMinute> <LatitudeSecond>0</LatitudeSecond> <LatitudeNpeerS>S</LatitudeNpeerS> <LongitudeDegree>153</LongitudeDegree> <LongitudeMinute>2</LongitudeMinute> <LongitudeSeconds>0</LongitudeSeconds> <LongitudeEperW>E</LongitudeEperW> </Table> <Table> <AirportCode>BNE</AirportCode> <CityOrAirportName>BRISBANE INTL</CityOrAirportName> <Country>Australia</Country> <CountryAbbrviation>AU</CountryAbbrviation> <CountryCode>802</CountryCode> <GMTOffset>-10</GMTOffset> <RunwayLengthFeet>11483</RunwayLengthFeet> <RunwayElevationFeet>13</RunwayElevationFeet> <LatitudeDegree>27</LatitudeDegree> <LatitudeMinute>28</LatitudeMinute> <LatitudeSecond>0</LatitudeSecond> <LatitudeNpeerS>S</LatitudeNpeerS> <LongitudeDegree>153</LongitudeDegree> <LongitudeMinute>2</LongitudeMinute> <LongitudeSeconds>0</LongitudeSeconds> <LongitudeEperW>E</LongitudeEperW> </Table> </NewDataSet>";
            var testKeyCache = "ABC";
            var testValEntity = new AirportInfoEntity { AirportCode = "ABC" };

            var service = Substitute.For<IAirportInfoService>();
            service.GetAirportInformationByAirportCode(testKey).Returns(testVal);
            
            var cache = Substitute.For<FlightTracker.Provider.ICache>();
            cache.GetCache<AirportInfoEntity>(testKeyCache).Returns(testValEntity);

            //from service
            var provider = Substitute.For<FlightTracker.Provider.AirportInfoProvider>(service, cache);
            provider.GetAirportInfoAsync(testKey);

            AirportInfoEntity eventData = null;

            provider.OnGetAirportInfoEventHandler += (code, entity, e) => eventData = entity;
            provider.OnGetAirportInfoEventHandler += Raise.Event<Action<string, AirportInfoEntity, Exception>>(testKey, testValEntity, null);
            
            Assert.AreEqual(testValEntity, eventData);

            //from cache
            Assert.AreEqual(testValEntity, provider.GetAirportInfo(testKeyCache));

            //no hit
            Assert.IsNull(provider.GetAirportInfo(null));
        }
    }
}
