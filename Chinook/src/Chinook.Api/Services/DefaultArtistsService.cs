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

		public IEnumerable<ArtistResource> GetArtists()
		{
			var artists = _context.Artist.OrderBy(a => a.Name);

			return Mapper.Map<IEnumerable<ArtistResource>>(artists);
		}

	}
}
