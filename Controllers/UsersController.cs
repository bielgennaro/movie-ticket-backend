using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MovieTicketApi.Data;
using MovieTicketApi.Models.DTOs;
using MovieTicketApi.Models.Entity;
using MovieTicketApi.Models.Request;
using MovieTicketApi.Services;

namespace MovieTicketApi.Controllers
{
    [Route( "api/users" )]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MovieTicketApiContext _context;
        private readonly TokenService _tokenService;
        private readonly PasswordHashService _passwordHashService;

        public UsersController( MovieTicketApiContext context, TokenService tokenService, PasswordHashService passwordHashService )
        {
            this._context = context ?? throw new ArgumentNullException( nameof( context ) );
            this._tokenService = tokenService ?? throw new ArgumentNullException( nameof( tokenService ) );
            this._passwordHashService = passwordHashService;
        }

        [HttpGet( "list" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUserList()
        {
            try
            {
                List<UserDto> users = await this._context.Users
                    .Select( u => new UserDto
                    {
                        Id = u.Id,
                        Email = u.Email,
                        IsAdmin = u.IsAdmin,
                        HashedPassword = u.HashedPassword
                    } )
                    .ToListAsync();

                return this.Ok( users );
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, new { error = "Erro no servidor", message = ex.Message } );
            }
        }


        [HttpGet( "list/{id:int}" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<ActionResult<UserDto>> GetUser( int id )
        {
            try
            {
                var user = await this._context.Users.FindAsync( id );

                if( user == null )
                {
                    return this.NotFound();
                }

                return this.Ok( new UserDto { Id = user.Id, Email = user.Email, IsAdmin = user.IsAdmin } );
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, new { error = "Erro no servidor", message = ex.Message } );
            }
        }

        [HttpPut( "edit/{id:int}" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError )]
        public async Task<IActionResult> PutUser( int id, [FromBody] CreateUserRequest userRequest )
        {
            try
            {
                var existingUser = await this._context.Users.FindAsync( id );

                if( existingUser == null )
                {
                    return this.NotFound();
                }

                existingUser.Email = userRequest.Email;
                existingUser.IsAdmin = userRequest.IsAdmin;
                existingUser.Password = userRequest.Password;

                this._context.Entry( existingUser ).State = EntityState.Modified;

                await this._context.SaveChangesAsync();

                return this.Ok( new { existingUser.Id } );
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, new { error = "Erro no servidor", message = ex.Message } );
            }
        }

        [HttpPost( "register" )]
        [ProducesResponseType( StatusCodes.Status201Created )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError )]
        public async Task<ActionResult<UserDto>> PostUser( CreateUserRequest request )
        {
            try
            {
                if( request == null )
                {
                    return this.BadRequest( new { error = "Requisição inválida" } );
                }

                if( await this._context.Users.AnyAsync( u => u.Email == request.Email ) )
                {
                    return this.BadRequest( new { error = "Email já está em uso" } );
                }

                string hashedPassword = this._passwordHashService.HashPassword( request.Password );

                var user = new User( request.Email, request.IsAdmin, request.Password, hashedPassword );

                this._context.Users.Add( user );
                await this._context.SaveChangesAsync();

                string jwtToken = this._tokenService.GenerateToken( user );

                return this.CreatedAtAction( nameof( GetUser ), new { id = user.Id }, new { auth = jwtToken } );
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, new { error = "Erro ao criar usuário", message = ex.Message } );
            }
        }




        [HttpDelete( "delete/{id:int}" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError )]
        public async Task<IActionResult> DeleteUser( int id )
        {
            try
            {
                var user = await this._context.Users.FindAsync( id );

                if( user == null )
                {
                    return this.NotFound();
                }

                this._context.Users.Remove( user );
                await this._context.SaveChangesAsync();

                return this.NoContent();
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, new { error = "Erro no servidor", message = ex.Message } );
            }
        }
    }
}
