using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MovieTicketApi.Data;
using MovieTicketApi.Models.DTOs;
using MovieTicketApi.Models.Entity;
using MovieTicketApi.Models.Request;
using MovieTicketApi.Services;

namespace MovieTicketApi.Controllers
{
    [Route( "/tickets" )]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly MovieTicketApiContext _context;

        public TicketsController( MovieTicketApiContext context )
        {
            this._context = context;
        }

        [HttpGet( "list" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicket()
        {
            try
            {
                var tickets = await this._context.Tickets.ToListAsync();
                return this.Ok( tickets );
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
            }
        }

        [HttpGet( "list/{id:int}" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<ActionResult<Ticket>> GetTicket( int id )
        {
            try
            {
                var ticket = await this._context.Tickets.FindAsync( id );

                if( ticket == null )
                {
                    throw new TicketNotFoundException();
                }

                return this.Ok( ticket );
            }
            catch( TicketNotFoundException )
            {
                return this.NotFound( "O ticket não foi encontrado." );
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
            }
        }

        [HttpPut( "edit/{id:int}" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        public async Task<IActionResult> PutTicket( int id, CreateTicketRequest request )
        {
            try
            {
                var ticket = new Ticket( request.SessionId, request.UserId, request.MovieId );

                if( !this.TicketExists( id ) )
                {
                    throw new TicketNotFoundException();
                }

                if( id != ticket.Id )
                {
                    throw new InvalidRequestException( "O ID no corpo da solicitação não corresponde ao ID da URL." );
                }

                this._context.Entry( ticket ).State = EntityState.Modified;
                await this._context.SaveChangesAsync();

                return this.NoContent();
            }
            catch( TicketNotFoundException )
            {
                return this.NotFound( "O ticket não foi encontrado." );
            }
            catch( InvalidRequestException ex )
            {
                return this.BadRequest( ex.Message );
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
            }
        }

        [HttpPost( "generate" )]
        [ProducesResponseType( StatusCodes.Status201Created )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        public async Task<ActionResult<TicketDto>> PostTicket( CreateTicketRequest request )
        {
            try
            {
                var ticket = new Ticket( request.UserId, request.MovieId, request.SessionId );
                var user = await this._context.Users.FindAsync( request.UserId );
                var session = await this._context.Sessions.FindAsync( request.SessionId );

                await this._context.Tickets.AddAsync( ticket );
                await this._context.SaveChangesAsync();

                if( session == null || user == null )
                {
                    throw new InvalidRequestException( "Sessão ou usuário não encontrados." );
                }

                await this._context.Tickets.AddAsync( ticket );
                await this._context.SaveChangesAsync();

                return this.CreatedAtAction( nameof( GetTicket ), new { id = ticket.Id }, ticket );
            }
            catch( InvalidRequestException ex )
            {
                return this.BadRequest( ex.Message );
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
            }
        }

        [HttpDelete( "delete/{id:int}" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> DeleteTicket( int id )
        {
            try
            {
                if( !this.TicketExists( id ) )
                {
                    throw new TicketNotFoundException();
                }

                var ticket = await this._context.Tickets.FindAsync( id );
                if( ticket == null )
                {
                    throw new TicketNotFoundException();
                }

                this._context.Tickets.Remove( ticket );
                await this._context.SaveChangesAsync();

                return this.NoContent();
            }
            catch( TicketNotFoundException )
            {
                return this.NotFound( "O ticket não foi encontrado." );
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
            }
        }

        private bool TicketExists( int id )
        {
            return this._context.Tickets.Any( e => e.Id == id );
        }
    }
}