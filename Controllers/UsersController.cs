#region

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTicketApi.Data;
using MovieTicketApi.Models;
using MovieTicketApi.Models.Dto;
using MovieTicketApi.Models.Requests;

#endregion

namespace MovieTicketApi.Controllers;

[Route("/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly MovieTicketApiContext _context;
    private readonly HashingService _hashingService;

    public UsersController(MovieTicketApiContext context, HashingService hashingService)
    {
        _context = context;
        _hashingService = hashingService;
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
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UserDto
            {
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                PasswordHash = user.PasswordHash
            };

            return Ok(userDto);
        }
    }

    // PUT: edit/5
    [HttpPut("edit/{id:int}")]
    [ProducesResponseType(StatusCodes.Status100Continue)]
    public async Task<IActionResult> PutUser(int id, User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        var existingUser = await _context.User.FindAsync(id);
        if (existingUser == null)
        {
            return NotFound();
        }

        byte[] salt = _hashingService.GenerateSalt();

        byte[] combinedBytes = _hashingService.CombinePasswordAndSalt(user.Password, salt);

        byte[] hash = _hashingService.ComputeHash(combinedBytes);

        string passwordHash = BitConverter.ToString(hash).Replace("-", "");

        existingUser.PasswordHash = passwordHash;
        existingUser.PasswordSalt = BitConverter.ToString(salt).Replace("-", "");

        _context.Entry(existingUser).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
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

    // POST: /create
    [HttpPost("create")]
    public async Task<ActionResult<UserDto>> PostUser(CreateUserRequest userRequest)
    {
        var user = new User(userRequest.Email, userRequest.isAdminn, userRequest.Password);

        if (_context.User == null)
            return Problem("Entity set is null.");

        var hashingService = new HashingService();

        byte[] salt = hashingService.GenerateSalt();

        byte[] combinedBytes = hashingService.CombinePasswordAndSalt(user.Password, salt);

        byte[] hash = hashingService.ComputeHash(combinedBytes);

        user.PasswordHash = BitConverter.ToString(hash).Replace("-", "");
        user.PasswordSalt = BitConverter.ToString(salt).Replace("-", "");

        _context.User.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { userId = user.Id });
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