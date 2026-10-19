using Kimera.Application.DTOs;
using Kimera.Application.Interfaces;
using Kimera.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kimera.Application.Services
{
    internal class MovieService : IMovieService
    {
        private readonly ITmdbService _tmdbService;
        private readonly IMovieRepository _movieRepository;
        public MovieService(ITmdbService tmdbService, IMovieRepository movieRepository)
        {
            _tmdbService = tmdbService;
            _movieRepository = movieRepository;
        }
        public async Task<Movie> ImportMovieDtoToMovieAsync(int TmdbId)
        {
            var dto = await  _tmdbService.GetMovieDetailsByIdAsync(TmdbId);
            var existing = await _movieRepository.GetByTmdbIdAsync((TmdbId));
            if(existing is not null)
            {
                return existing;
            }
            var movie = MapToEntity(dto);
            _movieRepository.AddAsync(movie);
            return movie;


        }
        private Movie MapToEntity(MovieDTO dto)
        {
            return new Movie(
                tmdbId: dto.TmdbId,
                title: dto.Title,
                originalTitle: dto.OriginalTitle,
                overview: dto.Overview,
                releaseDate: dto.ReleaseDate,
                runtime: dto.Runtime,
                originalLanguage: dto.OriginalLanguage,
                voteAverage: dto.VoteAverage,
                voteCount: dto.VoteCount,
                posterPath: dto.PosterPath,
                backdropPath: dto.BackdropPath
                );
            
        }
    }
}
