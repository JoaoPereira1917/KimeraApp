using Kimera.Application.Interfaces;
using Kimera.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Kimera.Infrastructure.Repository
{
    internal class MovieRepository : IMovieRepository
    {
        [Required]
        private readonly MovieContext _context;
        public MovieRepository(MovieContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Movie movie)
        {
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            return movie;
        }

        public async Task<Movie?> GetByTmdbIdAsync(int tmdbId)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.TmdbId == tmdbId);
            return movie;
        }

        public async Task<IEnumerable<Movie>> ListAsync()
        {
            var movies = await _context.Movies.ToListAsync();
            return movies;
        }

        public async Task UpdateAsync(Movie movie)
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }
    }
}

