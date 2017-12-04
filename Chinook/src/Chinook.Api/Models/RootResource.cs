namespace Chinook.Api.Models
{
	public class RootResource : Resource
    {
		public Link Artists
		{
			get; set;
		}

		public Link Albums
		{
			get; set;
		}
	}
}
