using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MovieTicketApi.Data;
using MovieTicketApi.Models.DTOs;
using MovieTicketApi.Models.Entity;
using MovieTicketApi.Models.Request;

namespace MovieTicketApi.Controllers
{
    [Route( "api/tickets" )]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly MovieTicketApiContext _context;

        public TicketsController( MovieTicketApiContext context )
        {
            this._context = context ?? throw new ArgumentNullException( nameof( context ) );
        }

        [HttpGet( "list" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        public async Task<ActionResult<IEnumerable<object>>> GetTickets()
        {
            try
            {
                var tickets = await this._context.Tickets
                    .Select( t => new
                    {
                        t.Id,
                        t.UserId,
                        t.SessionId
                    } )
                    .ToListAsync();

                return this.Ok( tickets );
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, new { error = "Erro no servidor", message = ex.Message } );
            }
        }


        [HttpGet( "list/{id:int}" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<ActionResult<Ticket>> GetTicket( int id )
        {
            try
            {
                var ticket = await this._context.Tickets
                    .Where( t => t.Id == id )
                    .Select( t => new
                    {
                        t.Id,
                        t.UserId,
                        t.SessionId
                    } )
                    .FirstOrDefaultAsync();

                if( ticket == null )
                {
                    return this.NotFound( "O ticket não foi encontrado." );
                }

                return this.Ok( ticket );
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, new { error = "Erro no servidor", message = ex.Message } );
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
                if( !this.TicketExists( id ) )
                {
                    return this.NotFound( "O ticket não foi encontrado." );
                }

                var ticket = new Ticket( request.SessionId, request.UserId );

                if( id != ticket.Id )
                {
                    return this.BadRequest( "O ID no corpo da solicitação não corresponde ao ID da URL." );
                }

                this._context.Entry( ticket ).State = EntityState.Modified;
                await this._context.SaveChangesAsync();

                return this.NoContent();
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, new { error = "Erro no servidor", message = ex.Message } );
            }
        }

        [HttpPost( "generate" )]
        [ProducesResponseType( StatusCodes.Status201Created )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        public async Task<ActionResult<TicketDto>> PostTicket( CreateTicketRequest request )
        {
            try
            {
                var ticket = new Ticket( request.UserId, request.SessionId );
                var user = await this._context.Users.FindAsync( request.UserId );
                var session = await this._context.Sessions.FindAsync( request.SessionId );

                if( session == null || user == null )
                {
                    return this.BadRequest( "Sessão ou usuário não encontrados." );
                }

                await this._context.Tickets.AddAsync( ticket );
                await this._context.SaveChangesAsync();

                return this.Ok( new { id = ticket.Id } );
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, new { error = "Erro no servidor", message = ex.Message } );
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
                    return this.NotFound( "O ticket não foi encontrado." );
                }

                var ticket = await this._context.Tickets.FindAsync( id );

                if( ticket == null )
                {
                    return this.NotFound( "O ticket não foi encontrado." );
                }

                this._context.Tickets.Remove( ticket );
                await this._context.SaveChangesAsync();

                return this.NoContent();
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, new { error = "Erro no servidor", message = ex.Message } );
            }
        }

        private bool TicketExists( int id )
        {
            return this._context.Tickets.Any( e => e.Id == id );
        }
    }
}
