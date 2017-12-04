using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chinook.Api.Controllers
{
	[Route("api/")]
    public class RootController : Controller
    {
		[HttpGet(Name = nameof(GetRoot))]
        public IActionResult GetRoot()
        {
			var response = new RootResource
			{
				Self = Link.Create(nameof(GetRoot)),
				Albums = Link.Create(nameof(AlbumsController.GetAlbums)),
				Artists = Link.Create(nameof(ArtistsController.GetArtists))
			};

			return Ok(response);
        }
    }
}