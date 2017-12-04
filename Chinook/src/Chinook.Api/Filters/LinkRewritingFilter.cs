using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

		public LinkRewritingFilter(IUrlHelperFactory urlHelperFactory)
		{
			_urlHelperFactory = urlHelperFactory;
		}

		public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{
			var objectResult = context.Result as ObjectResult;

			if (objectResult?.Value == null || objectResult?.StatusCode != (int)HttpStatusCode.OK)
			{
				await next();
				return;
			}

			var rewriter = new LinkRewriter(_urlHelperFactory.GetUrlHelper(context));

			RewriteAllLinks(objectResult.Value, rewriter);

			await next();

		}

		private IEnumerable<T> GetAll<T>(TypeInfo typeInfo, Func<TypeInfo, IEnumerable<T>> accessor)
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

		private void RewriteAllLinks(object model, LinkRewriter rewriter)
		{
			if (model == null)
				return;

			var modelProperties = GetAll(model.GetType().GetTypeInfo(), i => i.DeclaredProperties).Where(p => p.CanRead).ToArray();

			var linkProperties = modelProperties.Where(p => p.CanWrite && p.PropertyType == typeof(Link));

			foreach (var linkProperty in linkProperties)
			{
				var rewritten = rewriter.Rewrite(linkProperty.GetValue(model) as Link);

				if (rewritten == null)
					continue;

				linkProperty.SetValue(model, rewritten);

				if (linkProperty.Name == nameof(Resource.Self))
				{
					modelProperties.SingleOrDefault(p => p.Name == nameof(Link.Href))?.SetValue(model, rewritten.Href);
					modelProperties.SingleOrDefault(p => p.Name == nameof(Link.Method))?.SetValue(model, rewritten.Method);
					modelProperties.SingleOrDefault(p => p.Name == nameof(Link.Relation))?.SetValue(model, rewritten.Relation);
				}
			}

			var arrayProperties = modelProperties.Where(p => p.PropertyType.IsArray);
			RewriteLinksInArrays(arrayProperties, model, rewriter);

			var objectProperties = modelProperties.Except(linkProperties).Except(arrayProperties);
			RewriteLinksInNestedObjects(objectProperties, model, rewriter);

		}

		private void RewriteLinksInArrays(IEnumerable<PropertyInfo> arrayProperties, object model, LinkRewriter rewriter)
		{
			foreach (var arrayProperty in arrayProperties)
			{
				var array = arrayProperty.GetValue(model) as Array ?? new Array[0];

				foreach (var element in array)
				{
					RewriteAllLinks(element, rewriter);
				}
			}
		}

		private void RewriteLinksInNestedObjects(IEnumerable<PropertyInfo> objectProperties, object model, LinkRewriter rewriter)
		{
			foreach (var objectProperty in objectProperties)
			{
				if (objectProperty.PropertyType == typeof(string))
				{
					continue;
				}

				var typeInfo = objectProperty.PropertyType.GetTypeInfo();
				if (typeInfo.IsClass)
				{
					RewriteAllLinks(objectProperty.GetValue(model), rewriter);
				}
			}
		}

	}
}
