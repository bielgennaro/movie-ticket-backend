#region

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTicketApi.Data;
using MovieTicketApi.Models;

#endregion

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

        // GET: /sessions
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Session>>> GetSession()
        {
            return await _context.Session.Include(s => s.SessionMovies).ToListAsync();
        }

        // GET: list/5
        [HttpGet("list/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Session>> GetSession(int id)
        {
            if (_context.Session == null)
            {
                return NotFound();
            }

            var session = await _context.Session.FindAsync(id);

            if (session == null)
            {
                return NotFound();
            }

            return session;
        }

        // PUT: /sessions/edit/5
        [HttpPut("edit/{id}")]
        [ProducesResponseType(StatusCodes.Status100Continue)]
        public async Task<IActionResult> PutSession(int id, Session session)
        {
            if (id != session.SessionId)
            {
                return BadRequest();
            }

            _context.Entry(session).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: /create
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Session>> PostSession(Session session)
        {
            _context.Session.Add(session);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSessions", new { id = session.SessionId }, session);
        }

        // DELETE: delete/5
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteSession(int id)
        {
            if (_context.Session == null)
            {
                return NotFound();
            }

            var session = await _context.Session.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }

            _context.Session.Remove(session);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SessionExists(int id)
        {
            return (_context.Session?.Any(e => e.SessionId == id)).GetValueOrDefault();
        }
    }
}