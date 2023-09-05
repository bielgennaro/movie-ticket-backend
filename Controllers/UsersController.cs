using Microsoft.AspNetCore.Mvc;
using MovieTicketApi.Data;
using MovieTicketApi.Models.Dto;
using MovieTicketApi.Models.Requests;
using MovieTicketApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

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

    [HttpGet("list")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<User>>> GetUser()
    {
        try
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("list/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(new UserDto { Email = user.Email, IsAdmin = user.IsAdmin });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("edit/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PutUser(int id, [FromBody] CreateUserRequest userRequest)
    {
        try
        {
            var existingUser = await _context.Users.FindAsync(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(userRequest.Password))
            {
                byte[] salt = _hashingService.GenerateSalt();
                byte[] combinedBytes = _hashingService.CombinePasswordAndSalt(userRequest.Password, salt);
                byte[] hash = _hashingService.ComputeHash(combinedBytes);

                existingUser.PasswordHash = BitConverter.ToString(hash).Replace("-", "");
                existingUser.PasswordSalt = BitConverter.ToString(salt).Replace("-", "");
            }

            existingUser.Email = userRequest.Email;
            existingUser.IsAdmin = userRequest.IsAdmin;

            _context.Entry(existingUser).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(new { userId = existingUser.Id });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserDto>> PostUser(CreateUserRequest userRequest)
    {
        try
        {
            byte[] salt = _hashingService.GenerateSalt();
            byte[] combinedBytes = _hashingService.CombinePasswordAndSalt(userRequest.Password, salt);
            byte[] hash = _hashingService.ComputeHash(combinedBytes);

            BitConverter.ToString(hash).Replace("-", "");
            BitConverter.ToString(salt).Replace("-", "");

            var user = new User(userRequest.Email, userRequest.IsAdmin,userRequest.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, new UserDto { Email = user.Email, IsAdmin = user.IsAdmin });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("delete/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    private bool UserExists(int id)
    {
        return _context.Users.Any(e => e.Id == id);
    }
}
