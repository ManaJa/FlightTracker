using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Provider.Services
{
    public class FlightTrackService : FlightTracker.Provider.Services.IFlightTrackService
    {
        private FlightTrackerDBEntities _DataQuery;
        public FlightTrackService() {
            _DataQuery = new FlightTrackerDBEntities();
        }

        public List<AirPort> GetAirPortList() {
            _DataQuery.AirPorts = _DataQuery.Set<AirPort>();
            return _DataQuery.AirPorts.ToList();   
        }


    }
}
