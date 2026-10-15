using Kimera.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

using Kimera.Domain.Entities;
using Kimera.Application.Interfaces.Services;


namespace Kimera.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ITmdbService _tmdbService;
        public TestController(ITmdbService tmdbService)
        {
            _tmdbService = tmdbService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var movie = await _tmdbService.GetMovieDetailsByIdAsync(id);
            if(movie is null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

    }
}
