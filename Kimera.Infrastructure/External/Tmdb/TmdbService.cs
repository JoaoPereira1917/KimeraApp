using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Kimera.Application.DTOs;
using Kimera.Application.Interfaces;
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
        public async Task<IEnumerable<MovieDTO>> GetPopular70sMoviesAsync(int page = 1) {
            var request = new HttpRequestMessage(HttpMethod.Get, $"discover/movie?sort_by=vote_count.desc&primary_release_date.gte=1970-01-01&primary_release_date.lte=1979-12-31&language=pt-BR&page={page}");
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();

            var searchResult = JsonSerializer.Deserialize<TmdbPagedResponseDto<MovieDTO>>(jsonString, _jsonSerializerOptions)
                ?? throw new JsonException("TMDB returned an empty response.");
            return searchResult.Results;
        }
        

        

        // 3. Para busca filtrada (enriquece seu banco com dados específicos)
       // Task<IEnumerable<MovieDTO>> DiscoverMoviesAsync(int? genreId = null, int? year = null, int? minVoteCount = 100){ }
    }
}
