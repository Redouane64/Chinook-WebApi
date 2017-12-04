using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Chinook.Api.Models;
using Chinook.Api.Services;

namespace Chinook.Api.Controllers
{
	[Route("api/[controller]")]
    public class ArtistsController : Controller
    {
		private readonly IArtistsService _artistsService;
		private readonly IAlbumsService _albumsService;

		public ArtistsController(IArtistsService artistsService, IAlbumsService albumsService)
        {
			_artistsService = artistsService;
			_albumsService = albumsService;
		}

        // GET: api/Artists
        [HttpGet(Name = nameof(GetArtists))]
        public IActionResult GetArtists()
        {
			var artists = _artistsService.GetArtists();

			var link = Link.CreateCollection(nameof(GetArtists), null);

			var collection = new Collection<ArtistResource>()
			{
				Self = link,
				Value = artists.ToArray()
			};

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
		public IActionResult GetArtistAlbums([FromRoute] int id)
		{
			var albums = _albumsService.GetAlbumsForArtist(id);

			var link = Link.CreateCollection(nameof(GetArtistAlbums), null);

			var collection = new Collection<AlbumResource>()
			{
				Self = link,
				Value = albums.ToArray()
			};

			return Ok(collection);
		}



	}
}