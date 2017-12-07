using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Chinook.Api.Models;
using Chinook.Api.Services;
using Microsoft.Extensions.Options;

namespace Chinook.Api.Controllers
{
	[Route("api/[controller]")]
    public class ArtistsController : Controller
    {
		private readonly IArtistsService _artistsService;
		private readonly IAlbumsService _albumsService;
		private readonly PagingOptions _defaultPagingOptions;

		public ArtistsController(IArtistsService artistsService, 
									IAlbumsService albumsService,
									IOptions<PagingOptions> defaultPagingOptions)
        {
			_artistsService = artistsService;
			_albumsService = albumsService;
			_defaultPagingOptions = defaultPagingOptions.Value;
		}

        // GET: api/Artists
        [HttpGet(Name = nameof(GetArtists))]
        public IActionResult GetArtists([FromQuery]PagingOptions pagingOptions)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(new ApiError()
				{
					Message = "Invalid arguments provided.",
					Details = ModelState.FirstOrDefault(k => k.Value.Errors.Any()).Value.Errors.FirstOrDefault().ErrorMessage
				});
			}

			pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;
			pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;

			var artists = _artistsService.GetArtists(pagingOptions);

			var link = Link.CreateCollection(nameof(GetArtists), null);
			var collection = PagedCollection<ArtistResource>.CreatePagedCollection(link, artists.Items, artists.TotalSize, pagingOptions);

			return Ok(collection);
        }

        // GET: api/Artists/5
        [HttpGet("{id:int}", Name = nameof(GetArtist))]
        public IActionResult GetArtist([FromRoute] int id)
        {
			var artist = _artistsService.GetArtist(id);

			if (artist != null)
			{
				return Ok(artist);
			}

			return NotFound();
        }

		[HttpGet("{id:int}/albums", Name = nameof(GetArtistAlbums))]
		public IActionResult GetArtistAlbums([FromRoute] int id, [FromQuery]PagingOptions pagingOptions)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new ApiError()
				{
					Message = "Invalid arguments provided.",
					Details = ModelState.FirstOrDefault(k => k.Value.Errors.Any()).Value.Errors.FirstOrDefault().ErrorMessage
				});
			}

			pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;
			pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;

			var albums = _albumsService.GetAlbumsForArtist(id, pagingOptions);

			var link = Link.CreateCollection(nameof(GetArtistAlbums), null);

			var collection = PagedCollection<AlbumResource>.CreatePagedCollection(link, albums.Items, albums.TotalSize, pagingOptions);

			return Ok(collection);
		}



	}
}