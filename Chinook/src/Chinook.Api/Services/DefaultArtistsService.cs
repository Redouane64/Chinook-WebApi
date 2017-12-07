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
	public class DefaultArtistsService : IArtistsService
	{
		private readonly ChinookContext _context;

		public DefaultArtistsService(ChinookContext context)
		{
			_context = context;
		}

		public async Task<ArtistResource> GetArtistAsync(int id, CancellationToken cancellationToken = default)
		{
			var artist = await _context.Artist.FirstOrDefaultAsync(a => a.ArtistId == id, cancellationToken);

			return Mapper.Map<ArtistResource>(artist);
		}

		public async Task<PageResult<ArtistResource>> GetArtistsAsync(PagingOptions pagingOptions, 
																		CancellationToken cancellationToken = default)
		{
			var allArtists = _context.Artist.OrderBy(a => a.Name);

			var artists = await allArtists.Skip(pagingOptions.Offset.Value)
									.Take(pagingOptions.Limit.Value)
									.ProjectTo<ArtistResource>()
									.ToArrayAsync(cancellationToken);

			return new PageResult<ArtistResource>() { Items = artists, TotalSize = allArtists.Count() };
		}

	}
}
