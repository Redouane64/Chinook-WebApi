using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Chinook.Api.Models;

namespace Chinook.Api.Services
{
	public interface IArtistsService
    {
		Task<PageResult<ArtistResource>> GetArtistsAsync(PagingOptions pagingOptions, CancellationToken cancellationToken = default);
		Task<ArtistResource> GetArtistAsync(int id, CancellationToken cancellationToken = default);
    }
}
