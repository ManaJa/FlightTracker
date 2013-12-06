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
	public class AirportInfoProvider : FlightTracker.Provider.IAirportInfoProvider
	{
		private const string AIRPORT_INFO_CACHE_KEY = "B506D978-43BA-4C9F-9A43-C90E54F8FDC8";
		private Services.IAirportInfoService _Service = null;
		private ICache _Cache = null;

		private static HashSet<IAsyncResult> _ServiceAsyncResults = new HashSet<IAsyncResult>();

        public event Action<string, Entity.AirportInfoEntity, Exception> OnGetAirportInfoEventHandler;


        //public AirportInfoProvider()
        //{
        //    _Service = new Services.AirportInfoService();
        //    _Cache = new Cache();
        //    _Cache.Name = AIRPORT_INFO_CACHE_KEY;
        //}

        public AirportInfoProvider(Services.IAirportInfoService service, ICache cache) {
            _Service = service;
            _Cache = cache;
            _Cache.Name = AIRPORT_INFO_CACHE_KEY;
        }

		public Entity.AirportInfoEntity GetAirportInfo(string airportCode) {

			var obj = GetAirportInfoCache(airportCode);
			if (obj == null)
			{
				var xml = _Service.GetAirportInformationByAirportCode(airportCode);
				obj = GetAirportInfoFromXml(xml);
				if (obj != null)
					_Cache.SetCache(airportCode, obj);	
			}
			
			return obj;
		}

		public void GetAirportInfoAsync(string airportCode) {
			if (!IsAirportInfoRequested(airportCode) && GetAirportInfoCache(airportCode)==null)
			{
				var result = _Service.BegingetAirportInformationByAirportCode(airportCode, AirportInformationByAirportCodeCallback, airportCode);
				_ServiceAsyncResults.Add(result);
			}
		}

		public Entity.AirportInfoEntity GetAirportInfoCache(string airportCode)
		{
			return _Cache.GetCache<Entity.AirportInfoEntity>(airportCode);
		}
		public bool IsAnyAirportInfoRequesting()
		{
			return _ServiceAsyncResults.Any();
		}
        public int NumberOfAnyAirportInfoRequested()
        {
            return _ServiceAsyncResults.Count();
        }
		public bool IsAirportInfoRequested(string airportCode)
		{
			return _ServiceAsyncResults.Any(item => string.Equals(item.AsyncState as string, airportCode));
		}
		private void AirportInformationByAirportCodeCallback(IAsyncResult ar) { 
			if(!ar.IsCompleted) return;
			
			_ServiceAsyncResults.RemoveWhere(result => result == ar);
            
            var airportCode = ar.AsyncState as string;
            
            try
            {
                var xml = _Service.EndgetAirportInformationByAirportCode(ar);
                var airportInfo = GetAirportInfoFromXml(xml);
                if (airportInfo != null)
                {
                    _Cache.SetCache(airportCode, airportInfo);
                    if (OnGetAirportInfoEventHandler != null) OnGetAirportInfoEventHandler(airportCode, airportInfo, null);
                }
                else {
                    if (OnGetAirportInfoEventHandler != null) OnGetAirportInfoEventHandler(airportCode, null, new Exception(xml));
                }
            }
            catch (Exception e)
            {
                if (OnGetAirportInfoEventHandler != null) OnGetAirportInfoEventHandler(airportCode, null, e);
            }
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
