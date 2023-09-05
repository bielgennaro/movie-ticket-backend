using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTicketApi.Data;
using MovieTicketApi.Models;
using MovieTicketApi.Models.Dto;
using MovieTicketApi.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketApi.Controllers
{
    [Route("/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieTicketApiContext _context;

        public MoviesController(MovieTicketApiContext context)
        {
            _context = context;
        }

        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            try
            {
                var movies = await _context.Movies.ToListAsync();
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("list/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            try
            {
                var movie = await _context.Movies.FindAsync(id);

                if (movie == null)
                {
                    return NotFound();
                }

                return Ok(movie);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPut("edit/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutMovie(int id, CreateMovieRequest movieRequest)
        {
            try
            {
                var existingMovie = await _context.Movies.FindAsync(id);

                if (existingMovie == null)
                {
                    return NotFound();
                }

                if (string.IsNullOrEmpty(movieRequest.Title))
                {
                    return BadRequest("O título do filme é obrigatório.");
                }

                existingMovie.Title = movieRequest.Title;
                existingMovie.Gender = movieRequest.Gender;
                existingMovie.Director = movieRequest.Director;
                existingMovie.Synopsis = movieRequest.Synopsis;
                existingMovie.BannerUrl = movieRequest.BannerUrl;

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }

                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MovieDto>> PostMovie(CreateMovieRequest movieRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(movieRequest.Title))
                {
                    return BadRequest("O título do filme é obrigatório.");
                }

                var movie = new Movie
                {
                    Title = movieRequest.Title,
                    Gender = movieRequest.Gender,
                    Director = movieRequest.Director,
                    Synopsis = movieRequest.Synopsis,
                    BannerUrl = movieRequest.BannerUrl
                };

                await _context.Movies.AddAsync(movie);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, new { movieId = movie.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpDelete("delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            try
            {
                var movie = await _context.Movies.FindAsync(id);
                if (movie == null)
                {
                    return NotFound();
                }

                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno do servidor: {ex.Message}");
            }
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
