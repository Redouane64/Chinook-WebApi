using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chinook.Api.Data;
using Chinook.Api.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
				cfg.CreateMap<Artist, ArtistViewModel>()
					.ForMember(avm => avm.Albums, o => o.MapFrom(a => a.Album.Select(al => al.Title)));
				cfg.CreateMap<Album, AlbumViewModel>()
					.ForMember(avm => avm.Artist, o => o.MapFrom(a => a.Artist.Name));
			});
			services.AddScoped<IMapper>(f => mapperCfg.CreateMapper());

            services.AddMvc();
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
