using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Chinook.Api.Data;
using Chinook.Api.Filters;
using Chinook.Api.Infrastructure;
using Chinook.Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

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

			// configure automapper.
			var mapperCfg = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<Artist, ArtistModel>()
					.ForMember(avm => avm.Albums, o => o.MapFrom(a => a.Album.Select(al => al.Title)));
				cfg.CreateMap<Album, AlbumModel>()
					.ForMember(avm => avm.Artist, o => o.MapFrom(a => a.Artist.Name));
			});

			services.AddScoped<IMapper>(f => mapperCfg.CreateMapper());

            services.AddMvc(options =>
			{
				options.Filters.Add(typeof(JsonExceptionFilter));

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
