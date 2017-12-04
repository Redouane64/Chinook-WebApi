namespace Chinook.Api.Models
{
	public class ArtistResource : Resource
    {
		public string Name { get; set; }

		public Link Albums
		{
			get; set;
		}
	}
}
