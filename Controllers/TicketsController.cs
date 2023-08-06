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
    [Route ("/")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly MovieTicketApiContext _context;

        public TicketsController(MovieTicketApiContext context)
        {
            _context = context;
        }

        // GET: /tickets
        [HttpGet ("tickets")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicket()
        {
            if (_context.Ticket == null)
            {
                return NotFound();
            }

            return await _context.Ticket.ToListAsync();
        }

        // GET: Tickets/5
        [HttpGet("tickets/{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
            if (_context.Ticket == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        // PUT: tickets/edit/5
        [HttpPut("tickets/edit/{id}")]
        public async Task<IActionResult> PutTicket(int id, Ticket ticket)
        {
            if (id != ticket.TicketId)
            {
                return BadRequest();
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: /tickets/create
        [HttpPost ("tickets/create")]
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
            if (_context.Ticket == null)
            {
                return Problem("Entity set 'MovieTicketApiContext.Ticket'  is null.");
            }

            _context.Ticket.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicket", new { id = ticket.TicketId }, ticket);
        }

        // DELETE: tickets/5
        [HttpDelete("tickets/{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            if (_context.Ticket == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketExists(int id)
        {
            return (_context.Ticket?.Any(e => e.TicketId == id)).GetValueOrDefault();
        }
    }
}