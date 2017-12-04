using Chinook.Api.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Chinook.Api.Filters
{
	public class JsonExceptionFilter : IExceptionFilter
	{
		private readonly IHostingEnvironment _env;

		public JsonExceptionFilter(IHostingEnvironment env)
		{
			_env = env;
		}

		public void OnException(ExceptionContext context)
		{
			var error = new ApiError();

			if (_env.IsDevelopment())
			{
				error.Message = context.Exception.Message;
				error.Details = context.Exception.StackTrace;
			}
			else
			{
				error.Message = "Something went wrong in the server.";
				error.Details = context.Exception.Message;
			}

			context.Result = new ObjectResult(error) { StatusCode = 500 };
		}
	}
}
