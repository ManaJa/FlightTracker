using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace FlightTracker.Provider
{
	public class FlightTrackerProvider : FlightTracker.Provider.IFlightTrackerProvider
	{
		private Services.IFlightTrackService _Service;

        public FlightTrackerProvider(Services.IFlightTrackService service)
        {
            _Service = service;
		}

		/// <summary>
		/// Provide all flight tracks
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Entity.FlightTrackEntity> GetFlightTracks() {

			var airPorts = _Service.GetAirPortList();

			return airPorts.Select(airport => new Entity.FlightTrackEntity {
													OriginPort = airport.OriginPort.Trim(),
													DestinationPort = airport.DestinationPort.Trim(),	
													PortPair = airport.PortPair.Trim(),
											  });
		}

	}
}
