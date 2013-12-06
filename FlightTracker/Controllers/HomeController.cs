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
        //Provider.FlightTrackerProvider flightTrackProvider = new Provider.FlightTrackerProvider();
        //Provider.AirportInfoProvider airportInfoProvider = new Provider.AirportInfoProvider();

        Provider.IFlightTrackerProvider flightTrackProvider;
        Provider.IAirportInfoProvider airportInfoProvider;

        public HomeController(Provider.IFlightTrackerProvider flightTrack, Provider.IAirportInfoProvider airportInfo) {
            flightTrackProvider = flightTrack;
            airportInfoProvider = airportInfo;
        }

        //
        // GET: /Home/
        public ActionResult Index()
        {  
			var flightTracks = flightTrackProvider.GetFlightTracks();
			var airportInfos = GetAvailableAirportInforsFromTrack(flightTracks);

			ViewBag.FlightTracks = flightTracks;
			ViewBag.AirportInfos = airportInfos;

			return View();
        }

		public IEnumerable<Provider.Entity.AirportInfoEntity> GetAvailableAirportInforsFromTrack(IEnumerable<Provider.Entity.FlightTrackEntity> tracks) {

            var uniqueAirportCodes = tracks.Select(t => t.OriginPort)
                            .Union(tracks.Select(t => t.DestinationPort))
                            .Distinct();

			foreach (var code in uniqueAirportCodes)
			{
				var airportInfo = airportInfoProvider.GetAirportInfoCache(code);
                if(airportInfo!=null)
                yield return airportInfo;
			}
		}
    }
}
