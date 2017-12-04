using Chinook.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chinook.Api.Infrastructure
{
	public class LinkRewriter
    {
		private readonly IUrlHelper _urlHelper;

		public LinkRewriter(IUrlHelper urlHelper)
		{
			_urlHelper = urlHelper;
		}

		public Link Rewrite(Link original)
		{
			return new Link()
			{
				Href = _urlHelper.Link(original.RouteName, original.RouteValue),
				Method = original.Method,
				Relation = original.Relation
			};
		}
    }
}
