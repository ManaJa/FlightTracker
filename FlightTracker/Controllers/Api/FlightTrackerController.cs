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
		Provider.IAirportInfoProvider _AirPortprovider;// = new Provider.AirportInfoProvider();

        public AirPortInfoController(Provider.IAirportInfoProvider provider) {
            _AirPortprovider = provider;
        }

        public dynamic Get(string id)
        {
			return _AirPortprovider.GetAirportInfo(id);
        }

    }
}
