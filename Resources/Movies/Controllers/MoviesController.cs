using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MovieTicketApi.Data;
using MovieTicketApi.Request;
using MovieTicketApi.Resources.Movies.Models;

namespace MovieTicketApi.Resources.Movies.Controllers
{
    [Route( "api/movies" )]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieTicketApiContext _context;

        public MoviesController( MovieTicketApiContext context )
        {
            this._context = context ?? throw new ArgumentNullException( nameof( context ) );
        }

        [HttpGet( "list" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            try
            {
                List<Movie> movies = await this._context.Movies.ToListAsync();
                return this.Ok( movies );
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, new { error = "Erro interno do servidor", message = ex.Message } );
            }
        }

        [HttpGet( "list/{id:int}" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<ActionResult<Movie>> GetMovie( int id )
        {
            try
            {
                var movie = await this._context.Movies.FindAsync( id );

                if( movie == null )
                {
                    return this.NotFound();
                }

                return this.Ok( movie );
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, new { error = "Erro interno do servidor", message = ex.Message } );
            }
        }

        [HttpPut( "edit/{id:int}" )]
        [ProducesResponseType( StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> PutMovie( int id, CreateMovieRequest movieRequest )
        {
            try
            {
                var existingMovie = await this._context.Movies.FindAsync( id );

                if( existingMovie == null )
                {
                    return this.NotFound();
                }

                if( string.IsNullOrEmpty( movieRequest.Title ) )
                {
                    return this.BadRequest( "O título do filme é obrigatório." );
                }

                existingMovie.Title = movieRequest.Title;
                existingMovie.Gender = movieRequest.Gender;
                existingMovie.Director = movieRequest.Director;
                existingMovie.Synopsis = movieRequest.Synopsis;
                existingMovie.BannerUrl = movieRequest.BannerUrl;

                await this._context.SaveChangesAsync();

                return this.Ok();
            }
            catch( DbUpdateConcurrencyException )
            {
                if( !this.MovieExists( id ) )
                {
                    return this.NotFound();
                }

                throw;
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, new { error = "Erro interno do servidor", message = ex.Message } );
            }
        }

        [HttpPost( "add" )]
        [ProducesResponseType( StatusCodes.Status201Created )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        public async Task<ActionResult<MovieDto>> PostMovie( CreateMovieRequest movieRequest )
        {
            try
            {
                if( string.IsNullOrEmpty( movieRequest.Title ) )
                {
                    return this.BadRequest( "O título do filme é obrigatório." );
                }

                var movie = new Movie
                {
                    Title = movieRequest.Title,
                    Gender = movieRequest.Gender,
                    Director = movieRequest.Director,
                    Synopsis = movieRequest.Synopsis,
                    BannerUrl = movieRequest.BannerUrl
                };

                await this._context.Movies.AddAsync( movie );
                await this._context.SaveChangesAsync();

                return this.CreatedAtAction( nameof( GetMovie ), new { id = movie.Id }, new { movieId = movie.Id } );
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, new { error = "Erro interno do servidor", message = ex.Message } );
            }
        }

        [HttpDelete( "delete/{id:int}" )]
        [ProducesResponseType( StatusCodes.Status204NoContent )]
        [ProducesResponseType( StatusCodes.Status404NotFound )]
        public async Task<IActionResult> DeleteMovie( int id )
        {
            try
            {
                var movie = await this._context.Movies.FindAsync( id );
                if( movie == null )
                {
                    return this.NotFound();
                }

                this._context.Movies.Remove( movie );
                await this._context.SaveChangesAsync();

                return this.NoContent();
            }
            catch( Exception ex )
            {
                return this.StatusCode( StatusCodes.Status500InternalServerError, new { error = "Erro interno do servidor", message = ex.Message } );
            }
        }

        private bool MovieExists( int id )
        {
            return this._context.Movies.Any( e => e.Id == id );
        }
    }
}
