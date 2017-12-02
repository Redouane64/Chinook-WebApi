using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Chinook.Api.Models
{
    public class ApiError
    {
		public string Message
		{
			get; set;
		}

		public string Details
		{
			get; set;
		}

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
		[DefaultValue("")]
		public string Stacktrace
		{
			get; set;
		}
	}
}
