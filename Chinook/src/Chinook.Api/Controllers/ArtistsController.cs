using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Chinook.Api.Data;
using AutoMapper;
using Chinook.Api.Models;
using Chinook.Api.Services;

namespace Chinook.Api.Controllers
{
    [Route("api/[controller]")]
    public class ArtistsController : Controller
    {
		private readonly IArtistsService _artistsService;

		public ArtistsController(IArtistsService artistsService)
        {
			_artistsService = artistsService;
		}

        // GET: api/Artists
        [HttpGet(Name = nameof(GetArtists))]
        public IActionResult GetArtists()
        {
			var artists = _artistsService.GetArtists();

			return Ok(artists);
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

    }
}