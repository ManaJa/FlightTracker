using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace FlightTracker
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			var jsonFormatter = config.Formatters.JsonFormatter;
			//jsonFormatter.MediaTypeMappings.Add(new QueryStringMapping("format", "json", "application/json"));

			var jsonSetting = jsonFormatter.SerializerSettings;
			jsonSetting.NullValueHandling = NullValueHandling.Ignore;
			jsonSetting.ContractResolver = new CamelCasePropertyNamesContractResolver();
			//jsonSetting.Converters.Add(new StringEnumConverter());
		}
	}
}
