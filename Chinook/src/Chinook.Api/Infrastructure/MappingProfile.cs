using AutoMapper;
using Chinook.Api.Data;
using Chinook.Api.Models;

namespace Chinook.Api.Infrastructure
{
	public class MappingProfile : Profile
    {
		public MappingProfile()
		{
			CreateMap<Artist, ArtistResource>();
			CreateMap<Album, AlbumResource>();
		}
    }
}
