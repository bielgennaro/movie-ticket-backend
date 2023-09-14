using System.Data.Common;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MovieTicketApi.Data;
using MovieTicketApi.Models;
using MovieTicketApi.Models.Dto;
using MovieTicketApi.Models.Requests;
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
                var users = await this._context.Users.ToListAsync();
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

        [HttpPost( "create" )]
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

                return this.CreatedAtAction( nameof( GetUser ), new { id = user.Id, authorization = jwtToken } );
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
        public IActionResult LoginUser( [FromBody] User user )
        {
            try
            {
                if( user == null || string.IsNullOrEmpty( user.Email ) || string.IsNullOrEmpty( user.Password ) )
                {
                    return this.BadRequest( "Usuário ou senha inválidos." );
                }

                var existingUser = this._context.Users.FirstOrDefault( u => u.Email == user.Email );

                if( existingUser == null )
                {
                    return this.BadRequest( "Usuário não encontrado." );
                }

                if( !this.IsPasswordValid( existingUser.Password, user.Password ) )
                {
                    return this.BadRequest( "Senha incorreta." );
                }

                var token = this._tokenService.Generate( existingUser );

                return this.Ok( new { userId = existingUser.Id, authorization = token } );
            }
            catch( DbException ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
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
        private bool IsPasswordValid( string storedPasswordHash, string providedPassword )
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify( providedPassword, storedPasswordHash );
            }
            catch( Exception )
            {
                return false;
            }
        }
    }
}