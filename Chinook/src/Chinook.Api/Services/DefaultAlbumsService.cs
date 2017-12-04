using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Chinook.Api.Data;
using Chinook.Api.Models;

namespace Chinook.Api.Services
{
	public class DefaultAlbumsService : IAlbumsService
	{
		private readonly ChinookContext _context;

		public DefaultAlbumsService(ChinookContext context)
		{
			_context = context;
		}

		public AlbumResource GetAlbum(int id)
		{
			var album = _context.Album.FirstOrDefault(a => a.AlbumId == id);

			return Mapper.Map<AlbumResource>(album);
		}

		public IEnumerable<AlbumResource> GetAlbums()
		{
			var albums = _context.Album.OrderByDescending(a => a.Title);

			return Mapper.Map<IEnumerable<AlbumResource>>(albums);
		}
	}
}
