using Kimera.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Kimera.Domain.Entities;
using Kimera.Api.DTOs;

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
        public async Task<IActionResult> AddMovie([FromBody] CreateMovieRequest request)
        {
            var movie = new Movie
            (
                tmdbId: request.TmdbId,
                title: request.Title,
                originalTitle: request.OriginalTitle,
                overview: request.Overview,
                releaseDate: request.ReleaseDate,
                runtime: request.Runtime,
                originalLanguage: request.OriginalLanguage,
                voteAverage: request.VoteAverage,
                voteCount: request.VoteCount,
                posterPath: request.PosterPath,
                backdropPath: request.BackdropPath
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
