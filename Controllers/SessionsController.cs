using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTicketApi.Data;
using MovieTicketApi.Models;
using MovieTicketApi.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketApi.Controllers
{
    [Route("/sessions")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly MovieTicketApiContext _context;

        public SessionsController(MovieTicketApiContext context)
        {
            _context = context;
        }

        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Session>>> GetSession()
        {
            try
            {
                var sessions = await _context.Sessions.ToListAsync();
                return Ok(sessions);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("list/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Session>> GetSession(int id)
        {
            try
            {
                var session = await _context.Sessions.FindAsync(id);

                if (session == null)
                {
                    return NotFound();
                }

                return Ok(session);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPut("edit/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutSession(int id, CreateSessionRequest sessionRequest)
        {
            try
            {
                var existingSession = await _context.Sessions.FindAsync(id);

                if (existingSession == null)
                {
                    return NotFound();
                }

                var updateSession = new Session(
                    sessionRequest.DateTime,
                    sessionRequest.Room,
                    sessionRequest.Movie
                );

                existingSession.Room = sessionRequest.Room;
                existingSession.DateTime = sessionRequest.DateTime;

                updateSession.Movie.Id = sessionRequest.Movie.Id;

                _context.Update(existingSession);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionExists(id))
                {
                    return NotFound();
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao atualizar a sessão.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Session>> PostSession(CreateSessionRequest sessionRequest)
        {
            try
            {
                var movie = new Movie(sessionRequest.Movie.Title, sessionRequest.Movie.Gender, sessionRequest.Movie.Synopsis, sessionRequest.Movie.Director, sessionRequest.Movie.BannerUrl);
                var session = new Session(sessionRequest.DateTime, sessionRequest.Room, movie);

                await _context.Sessions.AddAsync(session);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetSession), new { id = session.Id }, session);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
            }
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSession(int id)
        {
            try
            {
                var session = await _context.Sessions.FindAsync(id);

                if (session == null)
                {
                    return NotFound();
                }

                _context.Sessions.Remove(session);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
            }
        }

        private bool SessionExists(int id)
        {
            return _context.Sessions.Any(e => e.Id == id);
        }
    }
}
