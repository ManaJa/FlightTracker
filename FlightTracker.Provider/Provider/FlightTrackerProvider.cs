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
	public class FlightTrackerProvider
	{
		private FlightTrackerDBEntities _DataQuery;

		public FlightTrackerProvider(){
			_DataQuery = new FlightTrackerDBEntities();
			_DataQuery.AirPorts = _DataQuery.Set<AirPort>();
		}

		/// <summary>
		/// Provide all flight tracks
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Entity.FlightTrackEntity> GetFlightTracks() {

			var airPorts = _DataQuery.AirPorts.ToList();

			return airPorts.Select(airport => new Entity.FlightTrackEntity {
													OriginPort = airport.OriginPort.Trim(),
													DestinationPort = airport.DestinationPort.Trim(),	
													PortPair = airport.PortPair.Trim(),
											  });
		}

	}
}
