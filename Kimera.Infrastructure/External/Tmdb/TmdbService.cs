using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Kimera.Application.DTOs;
using Kimera.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;


namespace Kimera.Infrastructure.External.Tmdb
{
    public class TmdbService(HttpClient httpClient) : ITmdbService
    {
        
        
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };
                      
       
        // 1. Para exibir os resultados da busca semântica (ESSENCIAL)
        public async Task<MovieDTO> GetMovieDetailsByIdAsync(int tmdbId) {

            var request = new HttpRequestMessage(HttpMethod.Get, $"movie/{tmdbId}?language=pt-BR");
            

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            
            var jsonString = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<MovieDTO>(jsonString, _jsonSerializerOptions
                 ?? throw new JsonException("TMDB returned an empty response."));
        }

        // 2. Para popular seu banco de dados inicial (ESSENCIAL PARA MVP)
        //Task<IEnumerable<MovieDTO>> GetPopularMoviesAsync(int page = 1) { }

        // 3. Para busca filtrada (enriquece seu banco com dados específicos)
       // Task<IEnumerable<MovieDTO>> DiscoverMoviesAsync(int? genreId = null, int? year = null, int? minVoteCount = 100){ }
    }
}
