namespace Chinook.Api.Models
{
	public class AlbumResource : Resource
    {
		public string Title { get; set; }
		public Link Artist { get; set; }
	}
}
