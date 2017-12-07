using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Chinook.Api.Data;
using Chinook.Api.Models;

namespace Chinook.Api.Services
{
	public class DefaultArtistsService : IArtistsService
	{
		private readonly ChinookContext _context;

		public DefaultArtistsService(ChinookContext context)
		{
			_context = context;
		}

		public ArtistResource GetArtist(int id)
		{
			var artist = _context.Artist.FirstOrDefault(a => a.ArtistId == id);

			return Mapper.Map<ArtistResource>(artist);
		}

		public PageResult<ArtistResource> GetArtists(PagingOptions pagingOptions)
		{
			var allArtists = _context.Artist.OrderBy(a => a.Name);

			var artists = allArtists.Skip(pagingOptions.Offset.Value).Take(pagingOptions.Limit.Value);

			var result = Mapper.Map<IEnumerable<ArtistResource>>(artists);

			return new PageResult<ArtistResource>() { Items = result.ToArray(), TotalSize = allArtists.Count() };
		}

	}
}
