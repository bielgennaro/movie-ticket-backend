#region

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTicketApi.Data;
using MovieTicketApi.Models;

#endregion

namespace MovieTicketApi.Controllers;

[Route("/tickets")]
[ApiController]
public class TicketsController : ControllerBase
{
    private readonly MovieTicketApiContext _context;

    public TicketsController(MovieTicketApiContext context)
    {
        _context = context;
    }

    // GET: tickets/
    [HttpGet("list")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Ticket>>> GetTicket()
    {
        if (_context.Ticket == null) return NotFound();

        return await _context.Ticket.ToListAsync();
    }

    // GET: list/5
    [HttpGet("list/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Ticket>> GetTicket(int id)
    {
        if (_context.Ticket == null) return NotFound();

        var ticket = await _context.Ticket.FindAsync(id);

        if (ticket == null) return NotFound();

        return ticket;
    }

    // PUT: edit/5
    [HttpPut("edit/{id}")]
    [ProducesResponseType(StatusCodes.Status100Continue)]
    public async Task<IActionResult> PutTicket(int id, Ticket ticket)
    {
        if (id != ticket.Id) return BadRequest();

        _context.Entry(ticket).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TicketExists(id)) return NotFound();

            throw;
        }

        return NoContent();
    }


    // POST: create
    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
    {
        if (_context.Ticket == null) return Problem("Entity set 'MovieTicketApiContext.Ticket'  is null.");

        _context.Ticket.Add(ticket);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTicket", new { id = ticket.Id }, ticket);
    }

    // DELETE: delete/5
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteTicket(int id)
    {
        if (_context.Ticket == null) return NotFound();

        var ticket = await _context.Ticket.FindAsync(id);
        if (ticket == null) return NotFound();

        _context.Ticket.Remove(ticket);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TicketExists(int id)
    {
        return (_context.Ticket?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}