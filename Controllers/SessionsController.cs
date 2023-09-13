using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MovieTicketApi.Data;
using MovieTicketApi.Models;
using MovieTicketApi.Models.Requests;

namespace MovieTicketApi.Controllers
{
    [Route( "/sessions" )]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly MovieTicketApiContext _context;

        public SessionsController( MovieTicketApiContext context )
        {
            this._context = context;
        }

        [HttpGet( "list" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        public async Task<ActionResult<IEnumerable<Session>>> GetSession()
        {
            try
            {
                // puxar os filmes junto com as sess�es
                var sessions = await this._context.Sessions.ToListAsync();

                foreach ( var session in sessions )
                {
                    session.Movie = await this._context.Movies.FindAsync( session.MovieId );

                    if ( session.Movie == null )
                    {
                        return this.NotFound( $"Filme com ID {session.MovieId} n�o encontrado." );
                    }
                }

                return this.Ok( sessions );
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}" );
            }
        }

        [HttpGet( "list/{id}" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        public async Task<ActionResult<Session>> GetSession( int id )
        {
            try
            {
                var session = await this._context.Sessions.FindAsync( id );

                if( session == null )
                {
                    return this.NotFound();
                }

                session.Movie = await this._context.Movies.FindAsync( session.MovieId );

                return this.Ok( session );
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}" );
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
                if( !this._context.Sessions.Any( e => e.Id == id ) )
                {
                    return this.NotFound();
                }

                return this.StatusCode( StatusCodes.Status500InternalServerError, "Erro interno ao atualizar a sess�o." );
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}" );
            }
        }

        [HttpPost( "create" )]
        [ProducesResponseType( StatusCodes.Status201Created )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError )]
        public async Task<ActionResult<Session>> PostSession( CreateSessionRequest sessionRequest )
        {
            try
            {
                var session = new Session( sessionRequest.DateTime, sessionRequest.Room, sessionRequest.MovieId );

                var movie = await this._context.Movies.FindAsync( sessionRequest.MovieId );
                if( movie == null )
                {
                    return this.NotFound( $"Filme com ID {sessionRequest.MovieId} n�o encontrado." );
                }

                session.Movie = movie;

                await this._context.Sessions.AddAsync( session );
                await this._context.SaveChangesAsync();

                return this.CreatedAtAction( nameof( GetSession ), new { id = session.Id } );
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}" );
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
                return this.StatusCode( StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}" );
            }
        }
    }
}
