using System;
namespace FlightTracker.Provider.Services
{
    public interface IAirportInfoService
    {
        IAsyncResult BegingetAirportInformationByAirportCode(string airportCode, AsyncCallback callback, object asyncState);
        string EndgetAirportInformationByAirportCode(IAsyncResult result);
        string GetAirportInformationByAirportCode(string airportCode);
    }
}
