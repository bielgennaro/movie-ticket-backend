using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MovieTicketApi.Data;
using MovieTicketApi.Models.DTOs;
using MovieTicketApi.Models.Entity;
using MovieTicketApi.Models.Request;
using MovieTicketApi.Services;

namespace MovieTicketApi.Controllers
{
    [Route( "/users" )]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MovieTicketApiContext _context;
        private readonly TokenService _tokenService;

        public UsersController( MovieTicketApiContext context, TokenService tokenService )
        {
            this._context = context;
            this._tokenService = tokenService;
        }


        [HttpGet( "list" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            try
            {
                var users = await this._context.Users
                    .Select( u => new { u.Id, u.Email } )
                    .ToListAsync();

                return this.Ok( users );
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
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

                return this.Ok( new UserDto { Email = user.Email, IsAdmin = user.IsAdmin } );
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
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
                return this.StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
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
                var user = new User( request.Email, request.IsAdmin, request.Password );

                this._context.Users.Add( user );
                await this._context.SaveChangesAsync();

                var tokenService = new TokenService();
                string jwtToken = tokenService.Generate( user );

                return this.Ok( new { user.Id, auth = jwtToken } );
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
            }
        }


        [HttpPost( "login" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError )]
        public IActionResult LoginUser( [FromBody] LoginRequest loginRequest )
        {
            try
            {
                if( loginRequest == null || string.IsNullOrEmpty( loginRequest.Email ) || string.IsNullOrEmpty( loginRequest.Password ) )
                {
                    return this.BadRequest( "Usuário ou senha inválidos." );
                }

                var existingUser = this._context.Users.FirstOrDefault( u => u.Email == loginRequest.Email );

                var passwordService = new PasswordHashService();

                if( passwordService.VerifyPassword( loginRequest.Password, existingUser.PasswordHash ) )
                {
                    var jwtToken = this._tokenService.Generate( existingUser );

                    return this.Ok( new { existingUser.Id, auth = jwtToken } );
                }
                else
                {
                    return this.BadRequest( "Senha incorreta." );
                }
            }
            catch( Exception )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, "Erro no servidor." );
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
                return this.StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
            }
        }
    }
}