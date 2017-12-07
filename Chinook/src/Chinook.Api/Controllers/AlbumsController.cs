using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Chinook.Api.Models;
using Chinook.Api.Services;
using Microsoft.Extensions.Options;

namespace Chinook.Api.Controllers
{
	[Route("api/[controller]")]
    public class AlbumsController : Controller
    {
		private readonly IAlbumsService _albumsService;
		private readonly PagingOptions _defaultPagingOptions;

		public AlbumsController(IAlbumsService albumsService, IOptions<PagingOptions> defaultPagingOptions)
        {
			_albumsService = albumsService;
			_defaultPagingOptions = defaultPagingOptions.Value;
		}

        // GET: api/Albums
        [HttpGet(Name = nameof(GetAlbums))]
        public IActionResult GetAlbums([FromQuery]PagingOptions pagingOptions)
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

			var albums = _albumsService.GetAlbums(pagingOptions);

			var link = Link.CreateCollection(nameof(GetAlbums));
			var collection = PagedCollection<AlbumResource>.CreatePagedCollection(link, albums.Items, albums.TotalSize, pagingOptions);

			return Ok(collection);
        }

        // GET: api/Albums/5
        [HttpGet("{id:int}", Name = nameof(GetAlbum))]
        public IActionResult GetAlbum([FromRoute] int id)
        {
			var album = _albumsService.GetAlbum(id);

			if (album != null)
			{
				return Ok(album);
			}

			return NotFound();
        }

    }
}