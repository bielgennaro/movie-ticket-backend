using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MovieTicketApi.Data;
using MovieTicketApi.Models.DTOs;
using MovieTicketApi.Models.Entity;
using MovieTicketApi.Models.Request;

namespace MovieTicketApi.Controllers
{
    [Route( "api/sessions" )]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly MovieTicketApiContext _context;

        public SessionsController( MovieTicketApiContext context )
        {
            this._context = context ?? throw new ArgumentNullException( nameof( context ) );
        }

        [HttpGet( "list" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError )]
        public async Task<ActionResult<IEnumerable<Session>>> GetSessions()
        {
            try
            {
                var sessions = await this._context.Sessions.Include( s => s.Movie ).ToListAsync();

                return this.Ok( sessions );
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, new { error = "Erro interno", message = ex.Message } );
            }
        }

        [HttpGet( "list/{id}" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError )]
        public async Task<ActionResult<Session>> GetSession( int id )
        {
            try
            {
                var session = await this._context.Sessions.Include( s => s.Movie ).FirstOrDefaultAsync( s => s.Id == id );

                if( session == null )
                {
                    return this.NotFound();
                }

                return this.Ok( session );
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, new { error = "Erro interno", message = ex.Message } );
            }
        }

        [HttpPut( "edit/{id}" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError )]
        public async Task<IActionResult> PutSession( int id, CreateSessionRequest sessionRequest )
        {
            try
            {
                var existingSession = await this._context.Sessions.FindAsync( id );

                if( existingSession == null )
                {
                    return this.NotFound();
                }

                existingSession.Room = sessionRequest.Room;
                existingSession.DateTime = sessionRequest.DateTime;
                existingSession.MovieId = sessionRequest.MovieId;

                this._context.Update( existingSession );
                await this._context.SaveChangesAsync();

                return this.Ok();
            }
            catch( DbUpdateConcurrencyException )
            {
                if( !this.SessionExists( id ) )
                {
                    return this.NotFound();
                }

                return this.StatusCode( StatusCodes.Status500InternalServerError, "Erro interno ao atualizar a sessão." );
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, new { error = "Erro interno", message = ex.Message } );
            }
        }

        [HttpPost( "create" )]
        [ProducesResponseType( StatusCodes.Status201Created )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError )]
        public async Task<ActionResult<SessionDto>> PostSession( CreateSessionRequest sessionRequest )
        {
            try
            {
                var movie = await this._context.Movies.FindAsync( sessionRequest.MovieId );

                if( movie == null )
                {
                    return this.NotFound( new { error = $"Filme com ID {sessionRequest.MovieId} não encontrado." } );
                }

                var session = new Session( sessionRequest.DateTime, sessionRequest.Room, sessionRequest.MovieId );

                this._context.Sessions.Add( session );
                await this._context.SaveChangesAsync();

                return this.CreatedAtAction( nameof( GetSession ), new { id = session.Id } );
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, new { error = "Erro interno", message = ex.Message } );
            }
        }

        [HttpDelete( "delete/{id}" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError )]
        public async Task<IActionResult> DeleteSession( int id )
        {
            try
            {
                var session = await this._context.Sessions.FindAsync( id );

                if( session == null )
                {
                    return this.NotFound();
                }

                this._context.Sessions.Remove( session );
                await this._context.SaveChangesAsync();

                return this.NoContent();
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, new { error = "Erro interno", message = ex.Message } );
            }
        }

        private bool SessionExists( int id )
        {
            return this._context.Sessions.Any( e => e.Id == id );
        }
    }
}
