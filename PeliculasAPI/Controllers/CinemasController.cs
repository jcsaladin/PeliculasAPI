using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Data;
using PeliculasAPI.Entities;
using PeliculasAPI.ViewModels.CinemasViewModels;

namespace PeliculasAPI.Controllers
{
    [Route("api/cines")]
    [ApiController]
    public class CinemasController : ControllerBase
    {
        private readonly DBContext _context;

        public CinemasController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CinemaReturnViewModel>>> GetCinemas()
        {
            var cinemas = await _context.Cinemas.Where(x => x.Archived == false).ToListAsync();

            List<CinemaReturnViewModel> result = new List<CinemaReturnViewModel>();

            foreach (var cinema in cinemas)
            {
                var cinemaVM = new CinemaReturnViewModel
                {
                   Id = cinema.Id,
                   Name = cinema.Name,
                   Address = cinema.Address,
                   Latitude = cinema.Latitude,
                   Longitude = cinema.Longitude,
                   Archived = cinema.Archived,
                };

                result.Add(cinemaVM);
            }

            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CinemaReturnViewModel>> GetCinemas(int id)
        {
            var cinema = await _context.Cinemas.FindAsync(id);

            if (cinema == null)
            {
                return NotFound();
            }

            var cinemaVM = new CinemaReturnViewModel
            {
                Id = cinema.Id,
                Name = cinema.Name,
                Address = cinema.Address,
                Latitude = cinema.Latitude,
                Longitude = cinema.Longitude,
                Archived = cinema.Archived,
            };

            return cinemaVM;
        }

        [HttpPost]
        public async Task<ActionResult<CinemaInputViewModel>> PostCinemas(CinemaInputViewModel cinemasVM)
        {
            var cinema = new Cinema
            {
                Name = cinemasVM.Name,
                Address = cinemasVM.Address,
                Latitude = cinemasVM.Latitude,
                Longitude = cinemasVM.Longitude,
                Archived = false
            };

            _context.Cinemas.Add(cinema);
            await _context.SaveChangesAsync();

            return Ok(cinema);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCinemas(int id, CinemaEditViewModel cinemasVM)
        {
            if (id != cinemasVM.Id)
            {
                return BadRequest();
            }

            var cinema = await _context.Cinemas.FirstOrDefaultAsync(x => x.Id == id);
            if (cinema == null)
            {
                return NotFound();
            }

            cinema.Name = cinemasVM.Name;
            cinema.Address = cinemasVM.Address;
            cinema.Latitude = cinemasVM.Latitude;
            cinema.Longitude = cinemasVM.Longitude;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CinemasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(cinema);
        }

        // DELETE: api/Genres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCinemas(int id)
        {
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(x => x.Id == id);
            if (cinema == null)
            {
                return NotFound();
            }

            cinema.Archived = true;

            await _context.SaveChangesAsync();

            Thread.Sleep(4000);

            return Ok(new OkMessageModel()
            {
                data = "success"
            });
        }

        private bool CinemasExists(int id)
        {
            return _context.Cinemas.Any(e => e.Id == id);
        }
    }
}
