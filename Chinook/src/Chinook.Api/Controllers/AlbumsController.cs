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

namespace Chinook.Api.Controllers
{
    [Route("api/[controller]")]
    public class AlbumsController : Controller
    {

		public AlbumsController()
        {
		}

        // GET: api/Albums
        [HttpGet(Name = nameof(GetAlbums))]
        public async Task<IActionResult> GetAlbums()
        {
			// TO DO:

			return Ok();
        }

        // GET: api/Albums/5
        [HttpGet("{id:int}", Name = nameof(GetAlbum))]
        public async Task<IActionResult> GetAlbum([FromRoute] int id)
        {
			// TO DO:

			return NotFound();
        }

    }
}