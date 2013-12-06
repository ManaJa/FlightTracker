using System;
namespace FlightTracker.Provider.Services
{
    public interface IFlightTrackService
    {
        System.Collections.Generic.List<FlightTracker.Provider.AirPort> GetAirPortList();
    }
}
