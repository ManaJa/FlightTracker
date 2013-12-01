using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FlightTracker.Controllers.Api
{
    public class AirPortInfoController : ApiController
    {
		Provider.AirportInfoProvider airPortprovider = new Provider.AirportInfoProvider();
		
        public dynamic Get(string id)
        {
			return airPortprovider.GetAirportInfo(id);
        }

    }
}
