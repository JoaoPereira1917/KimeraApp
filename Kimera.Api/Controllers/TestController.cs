using Kimera.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

using Kimera.Infrastructure.DTOs;
using Kimera.Domain.Entities;


namespace Kimera.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        public TestController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }


        [HttpGet("list")]
        public async Task<IActionResult> ListMovies()
        {
            var moviess = await _movieRepository.ListAsync();
            return Ok(moviess);
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddMovie([FromBody] MovieDTO request)
        {
            // Converte o DTO para a entidade de domínio antes de persistir
            var movie = new Movie(
                request.TmdbId,
                request.Title,
                request.OriginalTitle,
                request.Overview,
                request.ReleaseDate,
                request.Runtime,
                request.OriginalLanguage,
                request.VoteAverage,
                request.VoteCount,
                request.PosterPath,
                request.BackdropPath
            );

            await _movieRepository.AddAsync(movie);

            return CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            if(movie is null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

    }
}
