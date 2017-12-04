using Newtonsoft.Json;

namespace Chinook.Api.Models
{
	public abstract class Resource : Link
    {
		[JsonIgnore]
		public Link Self
		{
			get; set;
		}
	}
}
