using System;
using Microsoft.AspNetCore.Routing;
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
				First = self,
				Last = GetLastLink(self, size, pagingOptions),
				Previous = GetPreviousLink(self, size, pagingOptions),
				Next = GetNextLink(self, size, pagingOptions)
			};


		private static Link GetNextLink(Link self, int size, PagingOptions pagingOptions)
		{
			if (pagingOptions?.Limit == null)
				return null;
			if (pagingOptions?.Offset == null)
				return null;

			var limit = pagingOptions.Limit.Value;
			var offset = pagingOptions.Offset.Value;

			var next = offset + limit;
			if (next >= size)
				return null;

			var parameters = new RouteValueDictionary(self.RouteValue)
			{
				["limit"] = limit,
				["offset"] = next
			};

			var newLink = Link.CreateCollection(self.RouteName, parameters);
			return newLink;
		}

		private static Link GetLastLink(Link self, int size, PagingOptions pagingOptions)
		{
			if (pagingOptions?.Limit == null)
				return null;

			var limit = pagingOptions.Limit.Value;

			if (size <= limit)
				return null;

			var offset = Math.Ceiling((size - (double)limit) / limit) * limit;

			var parameters = new RouteValueDictionary(self.RouteValue)
			{
				["limit"] = limit,
				["offset"] = offset
			};
			var newLink = Link.CreateCollection(self.RouteName, parameters);

			return newLink;
		}

		private static Link GetPreviousLink(Link self, int size, PagingOptions pagingOptions)
		{
			if (pagingOptions?.Limit == null)
				return null;
			if (pagingOptions?.Offset == null)
				return null;

			var limit = pagingOptions.Limit.Value;
			var offset = pagingOptions.Offset.Value;

			if (offset == 0)
			{
				return null;
			}

			if (offset > size)
			{
				return GetLastLink(self, size, pagingOptions);
			}

			var previousPage = Math.Max(offset - limit, 0);

			if (previousPage <= 0)
			{
				return self;
			}

			var parameters = new RouteValueDictionary(self.RouteValue)
			{
				["limit"] = limit,
				["offset"] = previousPage
			};
			var newLink = Link.CreateCollection(self.RouteName, parameters);

			return newLink;
		}


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
