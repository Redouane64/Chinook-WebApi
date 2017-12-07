using System.Collections.Generic;
using Chinook.Api.Models;

namespace Chinook.Api.Services
{
	public interface IAlbumsService
	{
		PageResult<AlbumResource> GetAlbums(PagingOptions pagingOptions);
		AlbumResource GetAlbum(int id);
		PageResult<AlbumResource> GetAlbumsForArtist(int artistId, PagingOptions pagingOptions);
	}
}
