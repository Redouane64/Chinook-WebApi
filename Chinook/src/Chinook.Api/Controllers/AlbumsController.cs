using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Chinook.Api.Data;
using Chinook.Api.ViewModels;
using AutoMapper;

namespace Chinook.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Albums")]
    public class AlbumsController : Controller
    {
        private readonly ChinookContext _context;
		private readonly IMapper _mapper;

		public AlbumsController(ChinookContext context, IMapper mapper)
        {
            _context = context;
			_mapper = mapper;
		}

        // GET: api/Albums
        [HttpGet]
        public IEnumerable<AlbumViewModel> GetAlbum()
        {
            return _mapper.Map<IEnumerable<AlbumViewModel>>(_context.Album.Include(nameof(Album.Artist)));
        }

        // GET: api/Albums/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAlbum([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var album = await _context.Album.Include(nameof(Album.Artist)).SingleOrDefaultAsync(m => m.AlbumId == id);

            if (album == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AlbumViewModel>(album));
        }

    }
}