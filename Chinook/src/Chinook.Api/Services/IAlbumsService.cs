using System.Collections.Generic;
using Chinook.Api.Models;

namespace Chinook.Api.Services
{
	public interface IAlbumsService
	{
		IEnumerable<AlbumResource> GetAlbums();
		AlbumResource GetAlbum(int id);
	}
}
