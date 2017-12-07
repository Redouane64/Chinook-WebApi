using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Chinook.Api.Models;
using Chinook.Api.Services;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetAlbums([FromQuery]PagingOptions pagingOptions, CancellationToken cancellationToken)
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

			var albums = await _albumsService.GetAlbumsAsync(pagingOptions, cancellationToken);

			var link = Link.CreateCollection(nameof(GetAlbums));
			var collection = PagedCollection<AlbumResource>.CreatePagedCollection(link, albums.Items, albums.TotalSize, pagingOptions);

			return Ok(collection);
        }

        // GET: api/Albums/5
        [HttpGet("{id:int}", Name = nameof(GetAlbum))]
        public async Task<IActionResult> GetAlbum([FromRoute] int id, CancellationToken cancellationToken)
        {
			var album = await _albumsService.GetAlbumAsync(id, cancellationToken);

			if (album != null)
			{
				return Ok(album);
			}

			return NotFound();
        }

    }
}