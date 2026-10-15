using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Kimera.Application.DTOs;
using Kimera.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;


namespace Kimera.Infrastructure.External.Tmdb
{
    public class TmdbService : ITmdbService
    {
        private readonly string _apikey;
        private readonly string _bearerToken;
        private readonly HttpClient _httpClient;
        public TmdbService(IConfiguration configuration, HttpClient httpClient)
        {
            _apikey = configuration["TMDB:ApiKey"];
            _bearerToken = configuration["TMDB:BearerToken"];
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.themoviedb.org/3/");
        }
        // 1. Para exibir os resultados da busca semântica (ESSENCIAL)
        public async Task<MovieDTO> GetMovieDetailsByIdAsync(int tmdbId) {

            var request = new HttpRequestMessage(HttpMethod.Get, $"movie/{tmdbId}?language=pt-BR");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _bearerToken);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            
            var jsonString = await response.Content.ReadAsStringAsync();
            
            return JsonSerializer.Deserialize<MovieDTO>(jsonString,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
                }) ?? throw new JsonException("TMDB returned an empty response.");
        }

        // 2. Para popular seu banco de dados inicial (ESSENCIAL PARA MVP)
        //Task<IEnumerable<MovieDTO>> GetPopularMoviesAsync(int page = 1) { }

        // 3. Para busca filtrada (enriquece seu banco com dados específicos)
       // Task<IEnumerable<MovieDTO>> DiscoverMoviesAsync(int? genreId = null, int? year = null, int? minVoteCount = 100){ }
    }
}
