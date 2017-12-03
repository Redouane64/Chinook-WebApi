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

namespace Chinook.Api.Controllers
{
    [Route("api/[controller]")]
    public class ArtistsController : Controller
    {

		public ArtistsController()
        {
		}

        // GET: api/Artists
        [HttpGet(Name = nameof(GetArtists))]
        public async Task<IActionResult> GetArtists()
        {
			// TO DO:

			return Ok();
        }

        // GET: api/Artists/5
        [HttpGet("{id:int}", Name = nameof(GetArtist))]
        public async Task<IActionResult> GetArtist([FromRoute] int id)
        {
			// TO DO:

			return NotFound();
        }

    }
}