using Microsoft.EntityFrameworkCore;
using Kimera.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;


namespace Kimera.Infrastructure
{
    internal class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
        }
        public DbSet<Movie> Movies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().ToTable("Movies");
            modelBuilder.Entity<Movie>().HasKey(m => m.Id);
            modelBuilder.Entity<Movie>().Property(m => m.TmdbId).IsRequired();
            modelBuilder.Entity<Movie>().Property(m => m.Title).IsRequired();
            modelBuilder.Entity<Movie>().Property(m => m.OriginalTitle).IsRequired();
            modelBuilder.Entity<Movie>().Property(m => m.Overview).IsRequired();
            modelBuilder.Entity<Movie>().Property(m => m.ReleaseDate).IsRequired();
            modelBuilder.Entity<Movie>().Property(m => m.Runtime).IsRequired();
            modelBuilder.Entity<Movie>().Property(m => m.OriginalLanguage).IsRequired();
            modelBuilder.Entity<Movie>().Property(m => m.VoteAverage).IsRequired();
            modelBuilder.Entity<Movie>().Property(m => m.VoteCount).IsRequired();
            modelBuilder.Entity<Movie>().Property(m => m.PosterPath).IsRequired();
            modelBuilder.Entity<Movie>().Property(m => m.BackdropPath).IsRequired();
            modelBuilder.Entity<Movie>().Property(m => m.CreatedAt).IsRequired();
            modelBuilder.Entity<Movie>().Property(m => m.UpdatedAt).IsRequired();
        }


    }
}
