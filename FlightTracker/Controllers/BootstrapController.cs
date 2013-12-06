using Microsoft.Practices.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;

namespace FlightTracker.Controllers
{
    public class BootstrapController : Controller
    {

        //
        // GET: /Bootstrap/
        public ActionResult Index()
        {
            Bootstrap.RunBootstrap();

            //Redirect to caller page
            var refpage = System.Web.HttpContext.Current.Request.QueryString["ref"];
            if (Bootstrap.IsCompleted && !string.IsNullOrEmpty(refpage))
            {   
                return Redirect(refpage);
            }

            return View(Bootstrap.Status);
        }
    }

    public static class Bootstrap {

        public static List<Models.BootstrapModel> Status { get { return _status; } }
        public static bool IsCompleted { get; private set; }

        private static List<Models.BootstrapModel> _status = new List<Models.BootstrapModel>();
        private static Dictionary<string, int> _errorCount = new Dictionary<string, int>();
        private static Dictionary<string, int> _reqCount = new Dictionary<string, int>();

        private static void AddNewStatus(string message)
        {
            _status.Add(new Models.BootstrapModel { TimeStamp = DateTime.Now, Status = message });
        }

        public static void RunBootstrap()
        {  

            var airportInfoProvider = new FlightTracker.Provider.AirportInfoProvider(new Provider.Services.AirportInfoService(), new Provider.Cache());
            var flightTrackProvider = new Provider.FlightTrackerProvider(new Provider.Services.FlightTrackService());

            if(_status.Count==0)
                AddNewStatus("Bootstrap started.");

            if (airportInfoProvider.IsAnyAirportInfoRequesting()) return;


            var flightTracks = flightTrackProvider.GetFlightTracks();
            var uniqueAirportCodes = flightTracks.Select(t => t.OriginPort)
                                                    .Union(flightTracks.Select(t => t.DestinationPort))
                                                    .Distinct();

            var hasRequest = false;

            for (int i = 0; i < uniqueAirportCodes.Count(); i++) {
                var code = uniqueAirportCodes.ToList()[i];
                var total = uniqueAirportCodes.Count();

                if (airportInfoProvider.GetAirportInfoCache(code) == null //already in cache
                    && !_errorCount.Any(item => item.Key == code && item.Value > 3 //number of trying
                    && !airportInfoProvider.IsAirportInfoRequested(code))//already send request
                    )
                {
                    airportInfoProvider.OnGetAirportInfoEventHandler += OnGetAirportInfo;
                    airportInfoProvider.GetAirportInfoAsync(code);

                    string status = "Retrieving.. {0} ({1}/{2})";
                    AddNewStatus(String.Format(status, code, (i + 1), total));

                    hasRequest = true;
                    break;
                }
            }

            if (!hasRequest) IsCompleted = true;
        }

        static void OnGetAirportInfo(string code, Provider.Entity.AirportInfoEntity info, Exception exception)
        {
            string status = "";

            if (exception == null)
            {
                status = "Successed [" + code + "]: " + info.CityOrAirportName;
            }
            else
            {
                if (!_errorCount.ContainsKey(code)) _errorCount.Add(code, 0);
                else _errorCount[code]++;

                status = "Fail [" + code + "] : " + exception.Message;
            }
            AddNewStatus(status);

            if (IsCompleted) {
                AddNewStatus("Bootstrap completed");
            }else
                Bootstrap.RunBootstrap();
        }    

    }

}
