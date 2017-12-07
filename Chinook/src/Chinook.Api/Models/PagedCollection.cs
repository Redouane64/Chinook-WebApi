using Newtonsoft.Json;

namespace Chinook.Api.Models
{
	public class PagedCollection<T> : Collection<T>
	{
		public static PagedCollection<T> CreatePagedCollection(Link self, T[] value, int size, PagingOptions pagingOptions) =>
			new PagedCollection<T>()
			{
				Self = self,
				Value = value,
				Size = size,
				Offset = pagingOptions.Offset,
				Limit = pagingOptions.Limit,

				// TO DO: complete navigation properties.
			};
		/*
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public Link First
		{
			get; set;
		}

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public Link Last
		{
			get; set;
		}

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public Link Next
		{
			get; set;
		}

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public Link Previous
		{
			get; set;
		}
		*/
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public int? Limit
		{
			get; set;
		}

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public int? Offset
		{
			get; set;
		}
		
		public int Size
		{
			get; set;
		}
	}
}
