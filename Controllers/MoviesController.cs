using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTicketApi.Data;
using MovieTicketApi.Models;

namespace MovieTicketApi.Controllers
{
    [Route("/")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieTicketApiContext _context;

        public MoviesController(MovieTicketApiContext context)
        {
            _context = context;
        }

        // GET: /movies
        [HttpGet ("movies")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovie()
        {
            if (_context.Movie == null)
            {
                return NotFound();
            }

            return await _context.Movie.ToListAsync();
        }

        // GET: movies/5
        [HttpGet("movies/{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            if (_context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // PUT: movies/edit/5
        [HttpPut("moviees/edit/{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.MovieId)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: /movies/create
        [HttpPost ("movies/create")]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            if (_context.Movie == null)
            {
                return Problem("Entity set 'MovieTicketApiContext.Movie'  is null.");
            }

            _context.Movie.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.MovieId }, movie);
        }

        // DELETE: movies/delete/5
        [HttpDelete("movies/delete/{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (_context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return (_context.Movie?.Any(e => e.MovieId == id)).GetValueOrDefault();
        }
    }
}