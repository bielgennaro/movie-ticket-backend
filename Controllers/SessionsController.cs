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
    public class SessionsController : ControllerBase
    {
        private readonly MovieTicketApiContext _context;

        public SessionsController(MovieTicketApiContext context)
        {
            _context = context;
        }

        // GET: /sessions
        [HttpGet ("sessions")]
        public async Task<ActionResult<IEnumerable<Session>>> GetSession()
        {
            if (_context.Session == null)
            {
                return NotFound();
            }

            return await _context.Session.ToListAsync();
        }

        // GET: api/Sessions/5
        [HttpGet("{id}")]
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
        [HttpPut("/sessions/edit/{id}")]
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

        // POST: /sessions/create
        [HttpPost ("/sessions/create")]
        public async Task<ActionResult<Session>> PostSession(Session session)
        {
            if (_context.Session == null)
            {
                return Problem("Entity set 'MovieTicketApiContext.Session'  is null.");
            }

            _context.Session.Add(session);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSession", new { id = session.SessionId }, session);
        }

        // DELETE: /sessions/delete/5
        [HttpDelete("/sessions/delete/{id}")]
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