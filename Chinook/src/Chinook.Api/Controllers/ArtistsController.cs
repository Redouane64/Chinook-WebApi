using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Chinook.Api.Data;
using AutoMapper;
using Chinook.Api.ViewModels;

namespace Chinook.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Artists")]
    public class ArtistsController : Controller
    {
        private readonly ChinookContext _context;
		private readonly IMapper _mapper;

		public ArtistsController(ChinookContext context, IMapper mapper)
        {
            _context = context;
			_mapper = mapper;
		}

        // GET: api/Artists
        [HttpGet]
        public IEnumerable<ArtistViewModel> GetArtist()
        {
            return _mapper.Map<IEnumerable<ArtistViewModel>>(_context.Artist.Include(nameof(Artist.Album)));
        }

        // GET: api/Artists/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetArtist([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artist = await _context.Artist.Include(nameof(Artist.Album)).SingleOrDefaultAsync(m => m.ArtistId == id);

            if (artist == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ArtistViewModel>(artist));
        }

    }
}