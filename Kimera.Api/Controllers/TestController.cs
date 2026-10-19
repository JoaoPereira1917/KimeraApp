using Kimera.Application.DTOs;
using Kimera.Application.Interfaces;
using Kimera.Domain.Entities;
using Microsoft.AspNetCore.Mvc;


namespace Kimera.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ITmdbService _tmdbService;
        private readonly IMovieService _movieService;
        public TestController(ITmdbService tmdbService, IMovieService movieService)
        {
            _tmdbService = tmdbService;
            _movieService = movieService;
        }
        [HttpGet("{id}")] // example endpoint: /api/test/123
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var movie = await _tmdbService.GetMovieDetailsByIdAsync(id);
            if(movie is null)
            {
                return NotFound();
            }
            return Ok(movie);
        }
        [HttpPost("{id}")] // example endpoint: /api/test/123
        public async Task<IActionResult> AddMovieById([FromRoute] int id)
        {
            var movie = await _movieService.AddMovieDtoToMovieAsync(id);
            return CreatedAtAction(nameof(GetById), new { id = movie.TmdbId }, movie);
        }


        [HttpGet("popular-70s")] // example endpoint: /api/test/popular-70s?page=1
        public async Task<IActionResult> GetPopular70sMovies([FromQuery] int page = 1)
        {
            var movies = await _tmdbService.GetPopular70sMoviesAsync(page);
            return Ok(movies);
        }
        [HttpPost("Add70sMovies")] // example endpoint: /api/test/Add70sMovies?page=1
        //PRECISO GERAR UM DTO DIFERENTE, POIS O DTO DE FILME QUE EU CRIEI NA APLICAÇÃO NÃO TEM TODOS OS CAMPOS QUE O DTO DA API TEM,
        //ENTÃO PRECISO CRIAR UM DTO DIFERENTE PARA POPULAR O BANCO DE DADOS
        //LOGO QUANDO O METODO TENTA POPULAR, CAI NUMA EXCEÇÃO POIS O RUNTIME RETORNADO É 0, E ISSO VIOLA AS VALIDAÇÕES DA ENTIDADE
        public async Task<IActionResult> AddPopular70sMovies([FromQuery] int page = 1)
        {
            var moviesDtos = await _tmdbService.GetPopular70sMoviesAsync(page);
            await _movieService.AddMoviesDtos(moviesDtos);
            return CreatedAtAction(nameof(GetById), new { id = moviesDtos.First().TmdbId }, moviesDtos);
        }
        [HttpGet("MoviesNoBanco")]// example endpoint: /api/test/MoviesNoBanco
        public async Task<IActionResult> GetMoviesInDatabase()
        {
            var movies = await _movieService.ListDataBaseMovies();
            return Ok(movies);
        }
    }
}
