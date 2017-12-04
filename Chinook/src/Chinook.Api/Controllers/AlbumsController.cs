using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Chinook.Api.Data;
using Chinook.Api.Models;
using AutoMapper;
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

			return Ok(albums);
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