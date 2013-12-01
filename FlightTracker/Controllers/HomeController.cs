using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FlightTracker.Controllers
{
    public class HomeController : Controller
    {
		Provider.FlightTrackerProvider flightTrackProvider = new Provider.FlightTrackerProvider();
		Provider.AirportInfoProvider airportInfoProvider = new Provider.AirportInfoProvider();

        //
        // GET: /Home/
        public ActionResult Index()
        {
			var flightTracks = flightTrackProvider.GetFlightTracks();
			var airportInfos = GetAvailableAirportInforsFromTrack(flightTracks.Take(2));

			ViewBag.FlightTracks = flightTracks;
			ViewBag.AirportInfos = airportInfos;

			return View();
        }

		private IEnumerable<Provider.Entity.AirportInfoEntity> GetAvailableAirportInforsFromTrack(IEnumerable<Provider.Entity.FlightTrackEntity> tracks) {

			var uniqueAirportCodes = GetUniqueAirportCodes(tracks);

			foreach (var code in uniqueAirportCodes)
			{
				airportInfoProvider.GetAirportInfoBackground(code);
			}

			////wait for all background process
			//while (airportInfoProvider.IsAirportInfoRequesting()) { }

			//foreach (var code in uniqueAirportCodes)
			//{
			//	yield return airportInfoProvider.GetAirportInfo(code);
			//}
			return Enumerable.Empty<Provider.Entity.AirportInfoEntity>();
		}

		private string[] GetUniqueAirportCodes(IEnumerable<Provider.Entity.FlightTrackEntity> tracks)
		{
			var uniqueAirportCodes = tracks.Select(t => t.OriginPort)
							.Union(tracks.Select(t => t.DestinationPort))
							.Distinct();

			return uniqueAirportCodes.ToArray();
		}
    }
}
