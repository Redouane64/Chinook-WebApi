using System.Collections.Generic;
using Chinook.Api.Models;

namespace Chinook.Api.Services
{
	public interface IArtistsService
    {
		IEnumerable<ArtistResource> GetArtists();
		ArtistResource GetArtist(int id);
    }
}
