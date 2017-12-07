using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chinook.Api.Data;
using Chinook.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Chinook.Api.Services
{
	public class DefaultAlbumsService : IAlbumsService
	{
		private readonly ChinookContext _context;

		public DefaultAlbumsService(ChinookContext context)
		{
			_context = context;
		}

		public async Task<AlbumResource> GetAlbumAsync(int id, CancellationToken cancellationToken = default)
		{
			var album = await _context.Album.FirstOrDefaultAsync(a => a.AlbumId == id, cancellationToken);

			return Mapper.Map<AlbumResource>(album);
		}

		public async Task<PageResult<AlbumResource>> GetAlbumsAsync(PagingOptions pagingOptions, 
																	CancellationToken cancellationToken = default)
		{
			var allAlbums = _context.Album.OrderBy(a => a.Title);

			var albums = await allAlbums.Skip(pagingOptions.Offset.Value)
								  .Take(pagingOptions.Limit.Value)
								  .ProjectTo<AlbumResource>()
								  .ToArrayAsync(cancellationToken);

			return new PageResult<AlbumResource>() { Items = albums, TotalSize = allAlbums.Count() };
		}

		public async Task<PageResult<AlbumResource>> GetAlbumsForArtistAsync(int artistId, PagingOptions pagingOptions,
											CancellationToken cancellationToken = default)
		{
			var allAlbums = _context.Album.Where(a => a.ArtistId == artistId)
										.OrderBy(a => a.Title);

			var albums = await allAlbums.Skip(pagingOptions.Offset.Value)
								.Take(pagingOptions.Limit.Value)
								.ProjectTo<AlbumResource>()
								.ToArrayAsync(cancellationToken);

			return new PageResult<AlbumResource>()
			{
				Items = albums,
				TotalSize = allAlbums.Count()
			};
		}
	}
}
