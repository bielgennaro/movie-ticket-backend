#region

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTicketApi.Data;
using MovieTicketApi.Models;

#endregion

namespace MovieTicketApi.Controllers;

[Route("/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly MovieTicketApiContext _context;

    public UsersController(MovieTicketApiContext context)
    {
        _context = context;
    }

    // GET: /users
    [HttpGet("list")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<User>>> GetUser()
    {
        if (_context.User == null) return NotFound();

        return await _context.User.ToListAsync();
    }

    // GET: list/5
    [HttpGet("list/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        if (_context.User == null) return NotFound();

        var user = await _context.User.FindAsync(id);

        if (user == null) return NotFound();

        return user;
    }

    // PUT: edit/5
    [HttpPut("edit/{id:int}")]
    [ProducesResponseType(StatusCodes.Status100Continue)]
    public async Task<IActionResult> PutUser(int id, User user)
    {
        if (id != user.Id) return BadRequest();

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id)) return NotFound();

            throw;
        }

        return NoContent();
    }

    // POST: /create
    [HttpPost("create")]
    public async Task<ActionResult<User>> PostUser(User user)
    {
        if (_context.User == null) return Problem("Entity set 'MovieTicketApiContext.Movie' is null.");

        _context.User.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetUser", new { id = user.Id }, user);
    }

    // DELETE: delete/5
    [HttpDelete("delete/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteUser(int id)
    {
        if (_context.User == null) return NotFound();

        var user = await _context.User.FindAsync(id);
        if (user == null) return NotFound();

        _context.User.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserExists(int id)
    {
        return (_context.User?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}