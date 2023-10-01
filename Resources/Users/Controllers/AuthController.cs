using System.Data.Common;

using Microsoft.AspNetCore.Mvc;

using MovieTicketApi.Data;
using MovieTicketApi.Request;
using MovieTicketApi.Services;

namespace MovieTicketApi.Resources.Users.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly MovieTicketApiContext _context;
        private readonly PasswordHash _passwordHashService;
        private readonly ILogger<AuthController> _logger;

        public AuthController( MovieTicketApiContext context, TokenService tokenService, PasswordHash passwordHashService, ILogger<AuthController> logger )
        {
            this._context = context ?? throw new ArgumentNullException( nameof( context ) );
            this._passwordHashService = passwordHashService ?? throw new ArgumentNullException( nameof( passwordHashService ) );
        }

        [HttpPost( "login" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError )]
        public IActionResult LoginUser( [FromBody] LoginRequest loginRequest )
        {
            try
            {
                if( loginRequest == null )
                {
                    return this.BadRequest( "Usuário ou senha inválidos." );
                }

                var existingUser = this._context.Users.FirstOrDefault( u => u.Email == loginRequest.Email );

                if( existingUser == null )
                {
                    return this.BadRequest( "Usuário não encontrado." );
                }

                if( !this._passwordHashService.VerifyPassword( loginRequest.Password, existingUser.HashedPassword ) )
                {
                    return this.BadRequest( "Senha incorreta." );
                }

                return this.Ok( new { UserId = existingUser.Id } );
            }
            catch( DbException ex )
            {
                return this.StatusCode( 500, "Erro interno do servidor." );
            }
            catch( Exception ex )
            {
                return this.StatusCode( 500, "Erro interno do servidor." );
            }
        }


    }
}
