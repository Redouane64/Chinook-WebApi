using System.ComponentModel;
using Newtonsoft.Json;

namespace Chinook.Api.Models
{
	public class Link
	{
		public const string GetMethod = "GET";

		public static Link Create(string routeName, object routeValue = null) 
			=> new Link() { RouteName = routeName, RouteValue = routeValue };

		public static Link CreateCollection(string routeName, object routeValue)
			=> new Link()
			{
				RouteName = routeName,
				RouteValue = routeValue,
				Method = GetMethod,
				Relation = new string[] { "collection" }
			};

		[JsonProperty(Order = -4)]
		public string Href
		{
			get; set;
		}

		[JsonProperty(Order = -3, DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
		[DefaultValue(GetMethod)]
		public string Method
		{
			get; set;
		}

		[JsonProperty(Order = -2, PropertyName = "rel", NullValueHandling = NullValueHandling.Ignore)]
		public string[] Relation
		{
			get; set;
		}

		[JsonIgnore]
		public string RouteName
		{
			get; set;
		}

		[JsonIgnore]
		public object RouteValue
		{
			get; set;
		}
	}
}
