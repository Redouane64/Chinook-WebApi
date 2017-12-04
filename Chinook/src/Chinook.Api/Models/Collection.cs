namespace Chinook.Api.Models
{
	public class Collection<T> : Resource
    {
		public T[] Value
		{
			get; set;
		}
	}
}
