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
		public float GMTOffset {get;set;}//-10
		public int RunwayLengthFeet {get;set;}//11483
		public int RunwayElevationFeet {get;set;}//13
		public int LatitudeDegree {get;set;}//27
		public int LatitudeMinute {get;set;}//28
		public int LatitudeSecond {get;set;}//0
		public string LatitudeNpeerS {get;set;}//S
		public int LongtitudeDegree {get;set;}//153
		public int LongtitudeMinute {get;set;}//2
		public int LongtitudeSeconds {get;set;}//0
		public string LongtitudeEperW {get;set;}//E

        public decimal LatitudeDecimalDegree
        {
            get
            {
                //Decimal Degrees = Degrees + minutes/60 + seconds/3600
                var absDegree = Math.Abs(LatitudeDegree + LatitudeMinute / 60 + LatitudeSecond / 3600);
                return LatitudeNpeerS == "N" ? absDegree : -absDegree;
            }
        }
        public decimal LongtitudeDecimalDegree
        {
            get
            {
                //Decimal Degrees = Degrees + minutes/60 + seconds/3600
                var absDegree = Math.Abs(LongtitudeDegree + LongtitudeMinute / 60 + LongtitudeSeconds / 3600);
                return LongtitudeEperW == "E" ? absDegree : -absDegree;
            }
        }
    
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
		public float GMTOffset { get; set; }//-10
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

	internal static class AirPortEntityExtension {

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
				LongtitudeDegree = @this.LongitudeDegree,
				LongtitudeMinute = @this.LongitudeMinute,
				LongtitudeSeconds = @this.LongitudeSeconds,
				LongtitudeEperW = @this.LongitudeEperW,
			};
		}
	}

   

}
