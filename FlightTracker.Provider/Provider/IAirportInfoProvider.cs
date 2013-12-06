using System;
namespace FlightTracker.Provider
{
    public interface IAirportInfoProvider
    {
        global::FlightTracker.Provider.Entity.AirportInfoEntity GetAirportInfo(string airportCode);
        void GetAirportInfoAsync(string airportCode);
        global::FlightTracker.Provider.Entity.AirportInfoEntity GetAirportInfoCache(string airportCode);
        bool IsAirportInfoRequested(string airportCode);
        bool IsAnyAirportInfoRequesting();
        int NumberOfAnyAirportInfoRequested();
        event Action<string, global::FlightTracker.Provider.Entity.AirportInfoEntity, Exception> OnGetAirportInfoEventHandler;
    }
}
