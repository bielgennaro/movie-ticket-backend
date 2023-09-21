using System.Data.Common;

using Microsoft.AspNetCore.Mvc;

using MovieTicketApi.Data;
using MovieTicketApi.Models.Entity;
using MovieTicketApi.Services;

namespace MovieTicketApi.Controllers.Auth
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly MovieTicketApiContext _context;
        private readonly TokenService _tokenService;
        private readonly PasswordHashService _passwordHashService;
        private readonly ILogger<AuthController> _logger;

        public AuthController( MovieTicketApiContext context, TokenService tokenService, PasswordHashService passwordHashService, AuthController logger )
        {
            this._context = context ?? throw new ArgumentNullException( nameof( context ) );
            this._tokenService = tokenService ?? throw new ArgumentNullException( nameof( tokenService ) );
            this._passwordHashService = passwordHashService ?? throw new ArgumentNullException( nameof( passwordHashService ) );
        }

        [HttpPost( "login" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError )]
        public IActionResult LoginUser( [FromBody] User userLogin )
        {
            try
            {
                if( userLogin == null || string.IsNullOrEmpty( userLogin.Email ) || string.IsNullOrEmpty( userLogin.Password ) )
                {
                    this._logger.LogInformation( "Tentativa de login com dados inválidos." );
                    return this.BadRequest( "Usuário ou senha inválidos." );
                }

                var existingUser = this._context.Users.FirstOrDefault( u => u.Email == userLogin.Email );

                if( existingUser == null )
                {
                    this._logger.LogInformation( $"Usuário com email {userLogin.Email} não encontrado." );
                    return this.BadRequest( "Usuário não encontrado." );
                }

                if( !this._passwordHashService.VerifyPassword( userLogin.Password, existingUser.HashedPassword ) )
                {
                    this._logger.LogInformation( $"Senha incorreta para o usuário com email {userLogin.Email}." );
                    return this.BadRequest( "Senha incorreta." );
                }

                string token = this._tokenService.Generate( existingUser );

                this._logger.LogInformation( $"Login bem-sucedido para o usuário com email {userLogin.Email}." );
                return this.Ok( new { UserId = existingUser.Id, Authorization = token } );
            }
            catch( DbException ex )
            {
                this._logger.LogError( ex, "Erro durante a autenticação." );
                return this.StatusCode( 500, "Erro interno do servidor." );
            }
            catch( Exception ex )
            {
                this._logger.LogError( ex, "Erro durante a autenticação." );
                return this.StatusCode( 500, "Erro interno do servidor." );
            }
        }
    }
}
