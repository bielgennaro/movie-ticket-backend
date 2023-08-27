#region

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTicketApi.Data;
using MovieTicketApi.Models;
using MovieTicketApi.Models.Requests;

#endregion

namespace MovieTicketApi.Controllers;

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
        return await _context.Session.Include(s => s.MovieId).ToListAsync();
    }

    // GET: list/5
    [HttpGet("list/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Session>> GetSession(int id)
    {
        if (_context.Session == null) return NotFound();

        var session = await _context.Session.FindAsync(id);

        if (session == null) return NotFound();

        return session;
    }

    // PUT: /sessions/edit/5
    [HttpPut("edit/{id}")]
    [ProducesResponseType(StatusCodes.Status100Continue)]
    public async Task<IActionResult> PutSession(int id, CreateSessionRequest sessionRequest)
    {
        var existingSession = await _context.Session.FindAsync(id);

        if (existingSession == null) return NotFound();

        var updateSession = new Session(
            sessionRequest.DateTime,
            sessionRequest.Room
        );

        existingSession.Room = sessionRequest.Room;
        existingSession.DateTime = sessionRequest.DateTime;

        try
        {
            _context.Update(existingSession);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SessionExists(id)) return NotFound();

            throw;
        }

        return Ok();
    }

    // POST: /create
    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Session>> PostSession(CreateSessionRequest sessionRequest)
    {
        var session = new Session(sessionRequest.DateTime, sessionRequest.Room);

        await _context.Session.AddAsync(session);
        await _context.SaveChangesAsync();

        return Ok(new { sessionId = session.Id });
    }

    // DELETE: delete/5
    [HttpDelete("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteSession(int id)
    {
        if (_context.Session == null) return NotFound();

        var session = await _context.Session.FindAsync(id);
        if (session == null) return NotFound();

        _context.Session.Remove(session);

        return NoContent();
    }

    private bool SessionExists(int id)
    {
        return (_context.Session?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}