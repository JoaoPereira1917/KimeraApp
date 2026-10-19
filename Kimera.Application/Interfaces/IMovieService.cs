using Kimera.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kimera.Application.Interfaces
{
    internal interface IMovieService
    {
        public Task<Movie> ImportMovieDtoToMovieAsync(int TmdbId);
        //public Task<IEnumerable<Movie>> ImportPopularMoviesAsync(int page = 1);
    }
}
