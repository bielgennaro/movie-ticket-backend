using Microsoft.AspNetCore.Mvc;
using MovieTicketApi.Data;
using MovieTicketApi.Models.Requests;
using MovieTicketApi.Models;
using Microsoft.EntityFrameworkCore;
using MovieTicketApi.Models.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("/tickets")]
[ApiController]
public class TicketsController : ControllerBase
{
    private readonly MovieTicketApiContext _context;

    public TicketsController(MovieTicketApiContext context)
    {
        _context = context;
    }

    [HttpGet("list")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Ticket>>> GetTicket()
    {
        try
        {
            var tickets = await _context.Tickets.ToListAsync();
            return Ok(tickets);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("list/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Ticket>> GetTicket(int id)
    {
        try
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                throw new TicketNotFoundException();
            }

            return Ok(ticket);
        }
        catch (TicketNotFoundException)
        {
            return NotFound("O ticket não foi encontrado.");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("edit/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutTicket(int id, Ticket ticket)
    {
        try
        {
            if (!TicketExists(id))
            {
                throw new TicketNotFoundException();
            }

            if (id != ticket.Id)
            {
                throw new InvalidRequestException("O ID no corpo da solicitação não corresponde ao ID da URL.");
            }

            _context.Entry(ticket).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (TicketNotFoundException)
        {
            return NotFound("O ticket não foi encontrado.");
        }
        catch (InvalidRequestException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TicketDto>> PostTicket(CreateTicketRequest ticketRequest)
    {
        try
        {
            var session = await _context.Sessions.FindAsync(ticketRequest.SessionId);
            var user = await _context.Users.FindAsync(ticketRequest.UserId);

            if (session == null || user == null)
            {
                throw new InvalidRequestException("Sessão ou usuário não encontrados.");
            }

            var ticket = new Ticket(ticketRequest.Id, ticketRequest.SessionId, ticketRequest.UserId, session, user);

            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id }, ticket);
        }
        catch (InvalidRequestException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("delete/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTicket(int id)
    {
        try
        {
            if (!TicketExists(id))
            {
                throw new TicketNotFoundException();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                throw new TicketNotFoundException();
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (TicketNotFoundException)
        {
            return NotFound("O ticket não foi encontrado.");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    private bool TicketExists(int id)
    {
        return _context.Tickets.Any(e => e.Id == id);
    }
}
