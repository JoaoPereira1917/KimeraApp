using Kimera.Application.DTOs;
using Kimera.Application.Interfaces;
using Kimera.Domain.Entities;


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
        public async Task<Movie> AddMovieDtoToMovieAsync(int TmdbId)
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
        
        public async Task AddMoviesDtos(IEnumerable<MovieDTO> moviesDtos)
        {
            foreach (var dto in moviesDtos)
            {
                var existing = await _movieRepository.GetByTmdbIdAsync(dto.TmdbId);

                if (existing is null)
                {
                    var movie = MapToEntity(dto);
                    _movieRepository.AddAsync(movie);
                }
            }
            

        }
        public async Task<IEnumerable<Movie>> ListDataBaseMovies()
        {
            var movies = await _movieRepository.ListAsync();
            return movies;
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
