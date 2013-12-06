using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;

namespace FlightTrackUnitTest
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void T01_Index()
        {
           var flightProvider =  Substitute.For<FlightTracker.Provider.IFlightTrackerProvider>();
           var airportProvider = Substitute.For<FlightTracker.Provider.IAirportInfoProvider>();

           flightProvider.GetFlightTracks().Returns(new List<FlightTracker.Provider.Entity.FlightTrackEntity>() { new FlightTracker.Provider.Entity.FlightTrackEntity{ OriginPort="A", DestinationPort="B"}});
             

           airportProvider.GetAirportInfoCache("A").Returns(new FlightTracker.Provider.Entity.AirportInfoEntity { AirportCode = "A" });
           airportProvider.GetAirportInfoCache("B").Returns(new FlightTracker.Provider.Entity.AirportInfoEntity { AirportCode = "B" });

           var controller = new FlightTracker.Controllers.HomeController(flightProvider, airportProvider);

           controller.Index();

           flightProvider.Received(1).GetFlightTracks();
           airportProvider.DidNotReceive().GetAirportInfoCache("A"); //enum
           airportProvider.DidNotReceive().GetAirportInfoCache("B"); //enum
        }

        [TestMethod]
        public void T02_GetAvailableAirportInforsFromTrack()
        {
            var duplicateFlight = Substitute.For<FlightTracker.Provider.IFlightTrackerProvider>();
            var allAirportInfomation = Substitute.For<FlightTracker.Provider.IAirportInfoProvider>();

            duplicateFlight.GetFlightTracks().Returns(new List<FlightTracker.Provider.Entity.FlightTrackEntity>() 
                                { new FlightTracker.Provider.Entity.FlightTrackEntity { OriginPort = "A", DestinationPort = "B" }
                                , new FlightTracker.Provider.Entity.FlightTrackEntity { OriginPort = "A", DestinationPort = "B" } });
            
            allAirportInfomation.GetAirportInfoCache("A").Returns(new FlightTracker.Provider.Entity.AirportInfoEntity { AirportCode = "A" });
            allAirportInfomation.GetAirportInfoCache("B").Returns(new FlightTracker.Provider.Entity.AirportInfoEntity { AirportCode = "B" });

            var controller = new FlightTracker.Controllers.HomeController(duplicateFlight, allAirportInfomation);

            controller.GetAvailableAirportInforsFromTrack(duplicateFlight.GetFlightTracks()).ToList();
            
            allAirportInfomation.DidNotReceiveWithAnyArgs().GetAirportInfo(Arg.Any<string>());
            allAirportInfomation.DidNotReceiveWithAnyArgs().GetAirportInfoAsync(Arg.Any<string>());
            allAirportInfomation.Received(1).GetAirportInfoCache("A");
            allAirportInfomation.Received(1).GetAirportInfoCache("B");
            
        }
    }
}
