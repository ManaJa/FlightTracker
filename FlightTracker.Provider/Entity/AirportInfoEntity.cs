using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightTracker.Provider.Entity
{
	public class AirportInfoEntity
	{
		public string AirportCode {get;set;}//BNE
		public string CityOrAirportName {get;set;} //BRISBANE INTL
		public string Country {get;set;}//Australia
		public string CountryAbbrviation {get;set;}//AU
		public int CountryCode {get;set;}//802
		public int GMTOffset {get;set;}//-10
		public int RunwayLengthFeet {get;set;}//11483
		public int RunwayElevationFeet {get;set;}//13
		public int LatitudeDegree {get;set;}//27
		public int LatitudeMinute {get;set;}//28
		public int LatitudeSecond {get;set;}//0
		public string LatitudeNpeerS {get;set;}//S
		public int LongitudeDegree {get;set;}//153
		public int LongitudeMinute {get;set;}//2
		public int LongitudeSeconds {get;set;}//0
		public string LongitudeEperW {get;set;}//E
	}


	[CollectionDataContract(Name = "NewDataSet", ItemName="Table", Namespace = "")]
	internal class AirportInfoDataSetServiceObject: List<AirportInfoTableServiceObject>{}
	[DataContract(Name="Table",Namespace="")]
	internal class AirportInfoTableServiceObject {
		[DataMember(Name = "AirportCode",Order=1)]
		public string AirportCode { get; set; }//BNE
		[DataMember(Name = "CityOrAirportName", Order=2)]
		public string CityOrAirportName { get; set; } //BRISBANE INTL
		[DataMember(Name = "Country", Order = 3)]
		public string Country { get; set; }//Australia
		[DataMember(Name = "CountryAbbrviation", Order = 4)]
		public string CountryAbbrviation { get; set; }//AU
		[DataMember(Name = "CountryCode", Order = 5)]
		public int CountryCode { get; set; }//802
		[DataMember(Name = "GMTOffset", Order = 6)]
		public int GMTOffset { get; set; }//-10
		[DataMember(Name = "RunwayLengthFeet", Order = 7)]
		public int RunwayLengthFeet { get; set; }//11483
		[DataMember(Name = "RunwayElevationFeet", Order = 8)]
		public int RunwayElevationFeet { get; set; }//13
		[DataMember(Name = "LatitudeDegree", Order = 9)]
		public int LatitudeDegree { get; set; }//27
		[DataMember(Name = "LatitudeMinute", Order = 10)]
		public int LatitudeMinute { get; set; }//28
		[DataMember(Name = "LatitudeSecond", Order = 11)]
		public int LatitudeSecond { get; set; }//0
		[DataMember(Name = "LatitudeNpeerS", Order = 12)]
		public string LatitudeNpeerS { get; set; }//S
		[DataMember(Name = "LongitudeDegree", Order=13)]
		public int LongitudeDegree { get; set; }//153
		[DataMember(Name = "LongitudeMinute", Order = 14)]
		public int LongitudeMinute { get; set; }//2
		[DataMember(Name = "LongitudeSeconds", Order = 15)]
		public int LongitudeSeconds { get; set; }//0
		[DataMember(Name = "LongitudeEperW", Order = 16)]
		public string LongitudeEperW { get; set; }//E

	}

	internal static class AirPortEntityEntension {

		internal static AirportInfoEntity ToEntity(this AirportInfoTableServiceObject @this)
		{
			return new AirportInfoEntity
			{
				AirportCode = @this.AirportCode,
				CityOrAirportName = @this.CityOrAirportName,
				Country = @this.Country,
				CountryAbbrviation = @this.CountryAbbrviation,
				CountryCode = @this.CountryCode,
				GMTOffset = @this.GMTOffset,
				RunwayLengthFeet = @this.RunwayLengthFeet,
				RunwayElevationFeet = @this.RunwayElevationFeet,
				LatitudeDegree = @this.LatitudeDegree,
				LatitudeMinute = @this.LatitudeMinute,
				LatitudeSecond = @this.LatitudeSecond,
				LatitudeNpeerS = @this.LatitudeNpeerS,
				LongitudeDegree = @this.LongitudeDegree,
				LongitudeMinute = @this.LongitudeMinute,
				LongitudeSeconds = @this.LongitudeSeconds,
				LongitudeEperW = @this.LongitudeEperW,
			};
		}
	}

}
