using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using FlightTracker.Provider.Entity;

namespace FlightTracker.Provider
{
	public class AirportInfoProvider
	{
		private const string AIRPORT_INFO_CACHE_KEY = "B506D978-43BA-4C9F-9A43-C90E54F8FDC8";
		private AirportService.airportSoapClient _Service = null;
		private Cache _Cache = null;

		private static HashSet<IAsyncResult> _ServiceAsyncResults = new HashSet<IAsyncResult>();

		public AirportInfoProvider()
		{ 
			_Service = new AirportService.airportSoapClient();
			_Cache = new Cache(AIRPORT_INFO_CACHE_KEY);
		}

		public Entity.AirportInfoEntity GetAirportInfo(string airportCode) {

			var obj = GetAirportInfoCache(airportCode);
			if (obj == null)
			{
				var xml = _Service.getAirportInformationByAirportCode(airportCode);
				obj = GetAirportInfoFromXml(xml);
				if (obj != null)
					_Cache.SetCache(airportCode, obj);	
			}
			
			return obj;
		}

		public void GetAirportInfoBackground(string airportCode) {
			if (!IsAirportInfoRequested(airportCode) && GetAirportInfoCache(airportCode)==null)
			{
				var result = _Service.BegingetAirportInformationByAirportCode(airportCode, AirportInformationByAirportCodeCallback, airportCode);
				_ServiceAsyncResults.Add(result);
			}
		}

		private Entity.AirportInfoEntity GetAirportInfoCache(string airportCode)
		{
			return _Cache.GetCache<Entity.AirportInfoEntity>(airportCode);
		}
		private bool IsAirportInfoRequesting()
		{
			return _ServiceAsyncResults.Any();
		}
		private bool IsAirportInfoRequested(string airportCode)
		{
			return _ServiceAsyncResults.Any(item => string.Equals(item.AsyncState as string, airportCode));
		}
		private void AirportInformationByAirportCodeCallback(IAsyncResult ar) { 
			if(!ar.IsCompleted) return;
			
			_ServiceAsyncResults.RemoveWhere(result => result == ar);


			var xml = _Service.EndgetAirportInformationByAirportCode(ar);
			var airportCode = ar.AsyncState as string;

			var airportInfo = GetAirportInfoFromXml(xml);
			if (airportInfo!=null)
				_Cache.SetCache(airportCode, airportInfo);	
		}
		private Entity.AirportInfoEntity GetAirportInfoFromXml(string xml)
		{
			var serviceObject = xml.XmlDeserializeFromString<Entity.AirportInfoDataSetServiceObject>();
			if (serviceObject != null && serviceObject.Any())
				return serviceObject[0].ToEntity();

			return null;
		}
	}
}
