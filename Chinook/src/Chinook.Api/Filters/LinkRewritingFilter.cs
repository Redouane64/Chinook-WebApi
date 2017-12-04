using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Chinook.Api.Infrastructure;
using Chinook.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Chinook.Api.Filters
{
	public class LinkRewritingFilter : IAsyncResultFilter
	{
		private readonly IUrlHelperFactory _urlHelperFactory;
		private LinkRewriter _linkRewriter;

		public LinkRewritingFilter(IUrlHelperFactory urlHelperFactory)
		{
			_urlHelperFactory = urlHelperFactory;
		}

		public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{
			_linkRewriter = new LinkRewriter(_urlHelperFactory.GetUrlHelper(context));

			if (context.Result is ObjectResult result && result.StatusCode == (int)System.Net.HttpStatusCode.OK)
			{
				var model = result.Value;
				var ti = model.GetType().GetTypeInfo();

				var modelProperties = GetAll(ti, i => i.DeclaredProperties).Where(p => p.CanRead).ToArray();

				var linkProperties = modelProperties.Where(p => p.PropertyType == typeof(Link) && p.CanWrite);

				foreach (var property in linkProperties)
				{
					Link rewrittenLink = null;

					if (property.GetValue(model) is Link value)
					{
						rewrittenLink = _linkRewriter.Rewrite(value);

						property.SetValue(model, rewrittenLink);
					}

					if (property.Name == nameof(Resource.Self))
					{
						modelProperties.SingleOrDefault(p => p.Name == nameof(Link.Href))?.SetValue(model, rewrittenLink?.Href);
						modelProperties.SingleOrDefault(p => p.Name == nameof(Link.Method))?.SetValue(model, rewrittenLink?.Method);
						modelProperties.SingleOrDefault(p => p.Name == nameof(Link.Relation))?.SetValue(model, rewrittenLink?.Relation);
					}
				}

				var arrayProperties = modelProperties.Where(p => p.PropertyType.IsArray);

				// Helper Function

				IEnumerable<T> GetAll<T>(TypeInfo typeInfo, Func<TypeInfo, IEnumerable<T>> accessor)
				{
					while (typeInfo != null)
					{
						foreach (var t in accessor(typeInfo))
						{
							yield return t;
						}

						typeInfo = typeInfo.BaseType?.GetTypeInfo();
					}
				}
			}

			await next();
			return;
		}
	}
}
