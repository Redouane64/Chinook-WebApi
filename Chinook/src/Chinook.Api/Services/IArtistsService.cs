using System.Collections.Generic;
using Chinook.Api.Models;

namespace Chinook.Api.Services
{
	public interface IArtistsService
    {
		PageResult<ArtistResource> GetArtists(PagingOptions pagingOptions);
		ArtistResource GetArtist(int id);
    }
}
