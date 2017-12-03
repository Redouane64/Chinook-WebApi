using System.ComponentModel;
using Newtonsoft.Json;

namespace Chinook.Api.Models
{
	public class Link
	{
		public const string GetMethod = "GET";

		public static Link Create(string routeName, object routeValue = null) 
			=> new Link() { RouteName = routeName, RouteValue = routeValue };

		[JsonProperty(Order = -3)]
		public string Href
		{
			get; set;
		}

		[JsonProperty(Order = -2, DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
		[DefaultValue(GetMethod)]
		public string Method
		{
			get; set;
		}

		[JsonProperty(Order = -1, PropertyName = "rel", NullValueHandling = NullValueHandling.Ignore)]
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
