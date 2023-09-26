using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieTicketApi.Data;
using MovieTicketApi.Models.Entity;
using MovieTicketApi.Services;
using System;
using System.Data.Common;
using System.Linq;
using Microsoft.AspNetCore.Http;
using MovieTicketApi.Models.Request;

namespace MovieTicketApi.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly MovieTicketApiContext _context;
        private readonly TokenService _tokenService;
        private readonly PasswordHashService _passwordHashService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(MovieTicketApiContext context, TokenService tokenService, PasswordHashService passwordHashService, ILogger<AuthController> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _passwordHashService = passwordHashService ?? throw new ArgumentNullException(nameof(passwordHashService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult LoginUser([FromBody] LoginRequest loginRequest)
        {
            try
            {
                if (loginRequest == null)
                {
                    _logger.LogInformation("Tentativa de login com dados inválidos.");
                    return BadRequest("Usuário ou senha inválidos.");
                }

                var existingUser = _context.Users.FirstOrDefault(u => u.Email == loginRequest.Email);

                if (existingUser == null)
                {
                    _logger.LogInformation($"Usuário com email {loginRequest.Email} não encontrado.");
                    return BadRequest("Usuário não encontrado.");
                }

                if (!_passwordHashService.VerifyPassword(loginRequest.Password, existingUser.HashedPassword))
                {
                    _logger.LogInformation($"Senha incorreta para o usuário com email {loginRequest.Email}.");
                    return BadRequest("Senha incorreta.");
                }

                _logger.LogInformation($"Login bem-sucedido para o usuário com email {loginRequest.Email}.");
                return Ok(new { UserId = existingUser.Id});
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "Erro durante a autenticação.");
                return StatusCode(500, "Erro interno do servidor.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante a autenticação.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }
    }
}
