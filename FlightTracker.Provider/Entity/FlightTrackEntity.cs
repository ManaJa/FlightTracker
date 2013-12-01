using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Provider.Entity
{
	public class FlightTrackEntity
	{
		public string OriginPort { get; set; }
		public string DestinationPort { get; set; }
		public string PortPair { get; set; }
	}
}
