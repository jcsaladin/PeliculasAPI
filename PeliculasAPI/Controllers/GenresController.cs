using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Data;
using PeliculasAPI.Entities;
using PeliculasAPI.ViewModels;

namespace PeliculasAPI.Controllers
{
    [Route("api/generos")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly DBContext _context;

        public GenresController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            return await _context.Genres.Where(x => x.Archived == false).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGenres(int id)
        {
            var genres = await _context.Genres.FindAsync(id);

            if (genres == null)
            {
                return NotFound();
            }

            return genres;
        }

        [HttpPost]
        public async Task<ActionResult<GenreInputViewModel>> PostGenres(GenreInputViewModel genresVM)
        {
            var genre = new Genre
            {
                Name = genresVM.Name,
                Archived = genresVM.Archived
            };

            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            return Ok(genre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenres(int id, GenreEditViewModel genresVM)
        {
            if (id != genresVM.Id)
            {
                return BadRequest();
            }

            var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);
            if (genre == null)
            {
                return NotFound();
            }

            genre.Name = genresVM.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenresExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(genre);
        }

        // DELETE: api/Genres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenres(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);
            if (genre == null)
            {
                return NotFound();
            }

            genre.Archived = true;

            await _context.SaveChangesAsync();

            return Ok(new OkMessageModel()
            {
                data = "success"
            });
        }

        private bool GenresExists(int id)
        {
            return _context.Genres.Any(e => e.Id == id);
        }
    }
}
