#region

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTicketApi.Data;
using MovieTicketApi.Models;
using MovieTicketApi.Models.Requests;

#endregion

namespace MovieTicketApi.Controllers
{
    [Route("/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieTicketApiContext _context;

        public MoviesController(MovieTicketApiContext context)
        {
            _context = context;
        }

        // GET: /movies
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovie()
        {
            return await _context.Movie.Include(m => m.SessionsList).ToListAsync();
        }

        // GET: movies/5
        [HttpGet("list/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        [HttpPut("edit/{id}")]
        [ProducesResponseType(StatusCodes.Status100Continue)]
        public async Task<IActionResult> PutMovie(int id, CreateMovieRequest movieRequest)
        {
            var existingMovie = await _context.Movie.FindAsync(id);

            if (existingMovie == null)
            {
                return NotFound();
            }

            var updateMovie = new Movie(
                movieRequest.Name,
                movieRequest.Synopsis,
                movieRequest.BannerUrl,
                movieRequest.Genre,
                movieRequest.Director
            );

            existingMovie.Name = movieRequest.Name;
            existingMovie.Genre = movieRequest.Genre;
            existingMovie.Director = movieRequest.Director;
            try
            {
                _context.Update(existingMovie);
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

            return Ok();
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Movie>> PostMovie(CreateMovieRequest movieRequest)
        {
            var movie = new Movie(movieRequest.Name, movieRequest.Genre, movieRequest.Director, movieRequest.Synopsis, movieRequest.BannerUrl);

            await _context.Movie.AddAsync(movie);
            await _context.SaveChangesAsync();

            return Ok(new { movieId = movie.Id });
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
            return (_context.Movie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}