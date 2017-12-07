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

		public PageResult<AlbumResource> GetAlbums(PagingOptions pagingOptions)
		{
			var allAlbums = _context.Album.OrderBy(a => a.Title);

			var albums = allAlbums.Skip(pagingOptions.Offset.Value)
								  .Take(pagingOptions.Limit.Value);

			var result = Mapper.Map<IEnumerable<AlbumResource>>(albums);

			return new PageResult<AlbumResource>() { Items = result.ToArray(), TotalSize = allAlbums.Count() };
		}

		public PageResult<AlbumResource> GetAlbumsForArtist(int artistId, PagingOptions pagingOptions)
		{
			var allAlbums = _context.Album.Where(a => a.ArtistId == artistId)
										.OrderBy(a => a.Title);

			var albums = allAlbums.Skip(pagingOptions.Offset.Value).Take(pagingOptions.Limit.Value);

			var result = Mapper.Map<IEnumerable<AlbumResource>>(albums);

			return new PageResult<AlbumResource>()
			{
				Items = result.ToArray(),
				TotalSize = allAlbums.Count()
			};
		}
	}
}
