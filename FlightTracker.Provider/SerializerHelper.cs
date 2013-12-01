﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FlightTracker.Provider
{
	public static class SerializerHelper
	{
		public static T XmlDeserializeFromString<T>(this string xmlString)
		{
			if(string.IsNullOrEmpty(xmlString)) return default(T);

			xmlString = System.Text.RegularExpressions.Regex.Replace(xmlString, "\n|\r|\t", string.Empty);

			using (Stream stream = new MemoryStream())
			{
				byte[] data = System.Text.Encoding.UTF8.GetBytes(xmlString);
				stream.Write(data, 0, data.Length);
				stream.Position = 0;
				var deserializer = new System.Runtime.Serialization.DataContractSerializer(typeof(T));
				return (T)deserializer.ReadObject(stream);
			}

			//var serializer = new System.Runtime.Serialization.DataContractSerializer(typeof(T));
			//using (var reader = new MemoryStream(System.Text.Encoding.Default.GetBytes(xmlString)))
			//{
			//	return (T)serializer.ReadObject(reader);
			//}
		}

		public static T ToObject<T>(this string @this)
		{
			if (@this == null) return default(T);

			//@this = "<?xml version=\"1.0\"?>" + @this;

			System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(T));
			
			var reader = new StringReader(@this);
			return (T)x.Deserialize(reader);
		} 
	}
}
