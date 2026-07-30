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
            modelBuilder.Entity<Movie>().Property(m => m.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Movie>().HasIndex(m => m.TmdbId).IsUnique();
            modelBuilder.Entity<Movie>().Property(m => m.TmdbId).IsRequired();

            modelBuilder.Entity<Movie>().Property(m => m.Title).IsRequired()
                                                                .HasMaxLength(200);

            modelBuilder.Entity<Movie>().Property(m => m.OriginalTitle).IsRequired()    
                                                                        .HasMaxLength(200);

            modelBuilder.Entity<Movie>().Property(m => m.Overview).IsRequired();
            modelBuilder.Entity<Movie>().Property(m => m.ReleaseDate);
            modelBuilder.Entity<Movie>().Property(m => m.Runtime).IsRequired();
            modelBuilder.Entity<Movie>().Property(m => m.OriginalLanguage).IsRequired().HasMaxLength(2); 

            modelBuilder.Entity<Movie>().Property(m => m.VoteAverage).IsRequired();
            modelBuilder.Entity<Movie>().HasIndex(m => m.VoteAverage);

            modelBuilder.Entity<Movie>().Property(m => m.VoteCount).IsRequired();
            modelBuilder.Entity<Movie>().HasIndex(m => m.VoteCount);

            modelBuilder.Entity<Movie>().Property(m => m.PosterPath).IsRequired();
            modelBuilder.Entity<Movie>().Property(m => m.BackdropPath);

            modelBuilder.Entity<Movie>().Property(m => m.CreatedAt).IsRequired();
            modelBuilder.Entity<Movie>().Property(m => m.UpdatedAt).IsRequired();
        }


    }
}
