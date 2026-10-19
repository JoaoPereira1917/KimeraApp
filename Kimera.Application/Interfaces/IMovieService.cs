using Kimera.Application.DTOs;
using Kimera.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kimera.Application.Interfaces
{
    public interface IMovieService
    {
        public Task<Movie> AddMovieDtoToMovieAsync(int TmdbId);
        public Task AddMoviesDtos(IEnumerable<MovieDTO> moviesDtos);
        public Task<IEnumerable<Movie>> ListDataBaseMovies();
    }
}
