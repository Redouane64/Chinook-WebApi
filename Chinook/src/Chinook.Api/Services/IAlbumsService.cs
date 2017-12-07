using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Chinook.Api.Models;

namespace Chinook.Api.Services
{
	public interface IAlbumsService
	{
		Task<PageResult<AlbumResource>> GetAlbumsAsync(PagingOptions pagingOptions, CancellationToken cancellationToken = default);
		Task<AlbumResource> GetAlbumAsync(int id, CancellationToken cancellationToken = default);
		Task<PageResult<AlbumResource>> GetAlbumsForArtistAsync(int artistId, PagingOptions pagingOptions, CancellationToken cancellationToken = default);
	}
}
