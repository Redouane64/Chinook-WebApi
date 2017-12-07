using System.Linq;
using AutoMapper;
using Chinook.Api.Data;
using Chinook.Api.Filters;
using Chinook.Api.Infrastructure;
using Chinook.Api.Models;
using Chinook.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chinook.Api
{
	public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddDbContextPool<ChinookContext>(options =>
			{
				options.UseNpgsql(Configuration.GetConnectionString("chinook-dev-db"));
			});

			services.AddScoped<IAlbumsService, DefaultAlbumsService>();
			services.AddScoped<IArtistsService, DefaultArtistsService>();

			services.AddMvc(options =>
			{
				options.Filters.Add(typeof(JsonExceptionFilter));
				options.Filters.Add(typeof(LinkRewritingFilter));

				// alter default json formatter to support ion+json format.
				var jsonFormatter = options.OutputFormatters.OfType<JsonOutputFormatter>().Single();
				options.OutputFormatters.Remove(jsonFormatter);

				options.OutputFormatters.Add(new IonJsonFormatter(jsonFormatter));
			});

			services.AddRouting(options =>
			{
				options.LowercaseUrls = true;
			});

			services.AddApiVersioning(options =>
			{
				options.ApiVersionReader = new MediaTypeApiVersionReader();
				options.ApiVersionSelector = new DefaultApiVersionSelector(options);
				options.AssumeDefaultVersionWhenUnspecified = true;
				options.DefaultApiVersion = new ApiVersion(1, 0);
				options.ReportApiVersions = true;
			});

			services.AddAutoMapper();

			services.Configure<PagingOptions>(Configuration.GetSection("DefaultPagingOptions"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
