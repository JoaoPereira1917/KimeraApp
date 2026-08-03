using Kimera.Domain.Models;
namespace Kimera.Application.Interfaces
{
    public interface IMovieRepository
    {
        public Task AddAsync(Movie movie);

        public Task<Movie?> GetByIdAsync(int id);

        public Task<Movie?> GetByTmdbIdAsync(int tmdbId);         

        public Task UpdateAsync(Movie movie);

        public Task DeleteAsync(Movie movie);

        public Task<IEnumerable<Movie>> ListAsync();
        //CancellationToken 

    }
}
