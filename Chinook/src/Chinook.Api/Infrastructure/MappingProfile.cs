using AutoMapper;
using Chinook.Api.Controllers;
using Chinook.Api.Data;
using Chinook.Api.Models;

namespace Chinook.Api.Infrastructure
{
	public class MappingProfile : Profile
    {
		public MappingProfile()
		{
			CreateMap<Artist, ArtistResource>().ForMember(dest => dest.Self, options => options.MapFrom(a => Link.Create(nameof(ArtistsController.GetArtistAsync), new
			{
				id = a.ArtistId
			}))).ForMember(dest => dest.Albums, options => options.MapFrom(a => Link.Create(nameof(ArtistsController.GetArtistAlbums), new
			{
				id = a.ArtistId
			})));

			CreateMap<Album, AlbumResource>().ForMember(dest => dest.Self, options => options.MapFrom(a => Link.Create(nameof(AlbumsController.GetAlbum), new
			{
				id = a.AlbumId
			}))).ForMember(dest => dest.Artist, options => options.MapFrom(a => Link.Create(nameof(ArtistsController.GetArtistAsync), new
			{
				id = a.ArtistId
			})));
			
		}
    }
}
