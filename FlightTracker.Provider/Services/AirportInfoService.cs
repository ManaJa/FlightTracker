using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Provider.Services
{
    public class AirportInfoService : FlightTracker.Provider.Services.IAirportInfoService
    {
        private AirportService.airportSoapClient _Service = null;

        public AirportInfoService() {
            _Service = new AirportService.airportSoapClient();
        }

        public string GetAirportInformationByAirportCode(string airportCode)
        {
            return _Service.getAirportInformationByAirportCode(airportCode);
        }

        public System.IAsyncResult BegingetAirportInformationByAirportCode(string airportCode, System.AsyncCallback callback, object asyncState)
        {
            return _Service.BegingetAirportInformationByAirportCode(airportCode, callback, asyncState);
        }

        public string EndgetAirportInformationByAirportCode(System.IAsyncResult result)
        {
            return _Service.EndgetAirportInformationByAirportCode(result);  
        }
    }
}
