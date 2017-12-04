using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Chinook.Api.Models;
using Chinook.Api.Services;

namespace Chinook.Api.Controllers
{
	[Route("api/[controller]")]
    public class AlbumsController : Controller
    {
		private readonly IAlbumsService _albumsService;

		public AlbumsController(IAlbumsService albumsService)
        {
			_albumsService = albumsService;
		}

        // GET: api/Albums
        [HttpGet(Name = nameof(GetAlbums))]
        public IActionResult GetAlbums()
        {
			var albums = _albumsService.GetAlbums();

			var link = Link.CreateCollection(nameof(GetAlbums), null);

			var collection = new Collection<AlbumResource>()
			{
				Self = link,
				Value = albums.ToArray()
			};

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