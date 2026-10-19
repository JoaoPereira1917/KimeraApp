using System;
using System.Collections.Generic;
using System.Text;
using Kimera.Application.DTOs;


namespace Kimera.Application.Interfaces
{
    public interface ITmdbService
    {
        // 1. Para exibir os resultados da busca semântica (ESSENCIAL)
        Task<MovieDTO> GetMovieDetailsByIdAsync(int tmdbId);

        // 2. Para popular seu banco de dados inicial (ESSENCIAL PARA MVP)
        //Task<IEnumerable<MovieDTO>> GetPopularMoviesAsync(int page = 1);

        // 3. Para busca filtrada (enriquece seu banco com dados específicos)
        //Task<IEnumerable<MovieDTO>> DiscoverMoviesAsync(int? genreId = null, int? year = null, int? minVoteCount = 100);
    }
}
