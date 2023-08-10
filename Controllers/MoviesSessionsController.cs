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
    [Route("/movies-sessions")]
    [ApiController]
    public class MoviesSessionsController : ControllerBase
    {
        private readonly MovieTicketApiContext _context;

        public MoviesSessionsController(MovieTicketApiContext context)
        {
            _context = context;
        }

        // GET: api/MoviesSessions
        [HttpGet ("list")]
        public async Task<ActionResult<IEnumerable<MovieSession>>> GetMovieSessions()
        {
          if (_context.MovieSessions == null)
          {
              return NotFound();
          }
            return await _context.MovieSessions.ToListAsync();
        }

        // GET: api/MoviesSessions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieSession>> GetMovieSession(int id)
        {
          if (_context.MovieSessions == null)
          {
              return NotFound();
          }
            var movieSession = await _context.MovieSessions.FindAsync(id);

            if (movieSession == null)
            {
                return NotFound();
            }

            return movieSession;
        }

        // PUT: api/MoviesSessions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovieSession(int id, MovieSession movieSession)
        {
            if (id != movieSession.MovieSessionId)
            {
                return BadRequest();
            }

            _context.Entry(movieSession).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieSessionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MoviesSessions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost ("/create")]
        public async Task<ActionResult<MovieSession>> PostMovieSession(MovieSession movieSession)
        {
          if (_context.MovieSessions == null)
          {
              return Problem("Entity set 'MovieTicketApiContext.MovieSessions'  is null.");
          }
            _context.MovieSessions.Add(movieSession);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovieSession", new { id = movieSession.MovieSessionId }, movieSession);
        }

        // DELETE: api/MoviesSessions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieSession(int id)
        {
            if (_context.MovieSessions == null)
            {
                return NotFound();
            }
            var movieSession = await _context.MovieSessions.FindAsync(id);
            if (movieSession == null)
            {
                return NotFound();
            }

            _context.MovieSessions.Remove(movieSession);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        // GET: api/MoviesSessions/home
        [HttpGet("home")]
        public async Task<IActionResult> HomeMovieSession(int id)
        {
            if (_context.MovieSessions == null)
            {
                return NoContent();
            }
            
            var currentTimeWithTimeZone = DateTime.UtcNow;
            var movieSession = await _context.MovieSessions
                .Where(s => s.Session.DateTime >= currentTimeWithTimeZone)
                .GroupBy(s => s.MovieId)
                .Select(s => s.First())
                .ToListAsync();

            if (movieSession == null)
            {
                return NotFound();
            }

            return Ok(movieSession);
        }

        private bool MovieSessionExists(int id)
        {
            return (_context.MovieSessions?.Any(e => e.MovieSessionId == id)).GetValueOrDefault();
        }
    }
}
