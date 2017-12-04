using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace Chinook.Api.Infrastructure
{
	public class IonJsonFormatter : TextOutputFormatter
	{
		private readonly JsonOutputFormatter _jsonOutputFormatter;

		public IonJsonFormatter(JsonOutputFormatter jsonOutputFormatter)
		{
			_jsonOutputFormatter = jsonOutputFormatter;

			SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/ion+json"));
			SupportedEncodings.Add(Encoding.UTF8);
		}

		public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
		{
			return _jsonOutputFormatter.WriteResponseBodyAsync(context, selectedEncoding);
		}
	}
}
