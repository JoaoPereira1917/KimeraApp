
using FluentAssertions;
using Kimera.Infrastructure.Persistence.Configurations;
using Kimera.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using Kimera.Domain.Entities;

namespace Kimera.Tests.Repositories
{
    public class MovieRepositoryIntegrationTests : IAsyncLifetime
    {

        private readonly PostgreSqlContainer _postgresContainer;

        private MovieContext _context;
        private MovieRepository _repository;

        public MovieRepositoryIntegrationTests()
        {
            _postgresContainer = new PostgreSqlBuilder()
                 .WithImage("postgres:latest")
                 .WithDatabase("kimera_test_db")
                 .WithUsername("test_user")
                 .WithPassword("test_pass")
                 .Build();
        }
        //iniciando o container
        public async Task InitializeAsync()
        {
            await _postgresContainer.StartAsync();

            var option = new DbContextOptionsBuilder<MovieContext>()
                .UseNpgsql(_postgresContainer.GetConnectionString())
                .Options;

            _context = new MovieContext(option);
            await _context.Database.EnsureCreatedAsync();
            _repository = new MovieRepository(_context);
        }
        //metodo pós testes
        public async Task DisposeAsync()
        {
            await _context.DisposeAsync();
            await _postgresContainer.DisposeAsync();
        }
        [Fact]
        public async Task AddAsync_ShouldInsertMovieAndReturnWithGeneratedId()
        {
            await InitializeAsync();
            var movie = new Movie(
                tmdbId: 123,
                title: "Test Movie",
                originalTitle: "Test Movie Original",
                overview: "This is a test movie.",
                releaseDate: new DateOnly(2023, 1, 1),
                runtime: 120,
                originalLanguage: "en",
                voteAverage: 8.5m,
                voteCount: 1000,
                posterPath: "/testposter.jpg",
                backdropPath: "/testbackdrop.jpg"
            );
            await _repository.AddAsync(movie);
            var savedMovie = _context.Movies.FirstOrDefault(m => m.TmdbId == 123);
            savedMovie.Should().NotBeNull();
            savedMovie!.Id.Should().BeGreaterThan(0);
            savedMovie.Title.Should().Be("Test Movie");
            savedMovie.Runtime.Should().Be(120);

        }
        [Fact]
        public async Task GetByTmdbIdAsync_WhenMovieExists_ShouldReturnCorrectMovie()
        {
            //arrange 
            var movie = new Movie(
                tmdbId: 456,
                title: "Another Test Movie",
                originalTitle: "Another Test Movie Original",
                overview: "This is another test movie.",
                releaseDate: new DateOnly(2023, 2, 1),
                runtime: 130,
                originalLanguage: "en",
                voteAverage: 7.5m,
                voteCount: 500,
                posterPath: "/anotherposter.jpg",
                backdropPath: "/anotherbackdrop.jpg"
            );
            await _repository.AddAsync(movie);
            //act
            var retrievedMovie = await _repository.GetByTmdbIdAsync(456);


            //assert

            retrievedMovie.Should().NotBeNull();
            retrievedMovie!.TmdbId.Should().Be(456);
            retrievedMovie.Title.Should().Be("Another Test Movie");

        }
        [Fact]
        public async Task UpdateAsync_ShouldUpdateMovieProperties()
        {
            var movie = new Movie(
               tmdbId: 777,
               title: "Another Test Movie: O teste agora é outro",
               originalTitle: "Another Test Movie Original",
               overview: "This is another test movie, ou será que não.",
               releaseDate: new DateOnly(2023, 2, 1),
               runtime: 130,
               originalLanguage: "en",
               voteAverage: 7.5m,
               voteCount: 500,
               posterPath: "/anotherposter.jpg",
               backdropPath: "/anotherbackdrop.jpg"
           );
            await _repository.AddAsync(movie);
            var savedMovie = await _repository.GetByTmdbIdAsync(777);

            //string overview, string posterPath, string? backdropPath
            savedMovie.Should().NotBeNull();
            savedMovie!.Overview.Should().Be("This is another test movie, ou será que não.");
            savedMovie.PosterPath.Should().Be("/anotherposter.jpg");
            savedMovie.BackdropPath.Should().Be("/anotherbackdrop.jpg");

            savedMovie.UpdateDetails("Updated overview", "/updatedposter.jpg", "/updatedbackdrop.jpg");
            savedMovie.Should().NotBeNull();
            savedMovie.Overview.Should().Be("Updated overview");
            savedMovie.PosterPath.Should().Be("/updatedposter.jpg");
            savedMovie.BackdropPath.Should().Be("/updatedbackdrop.jpg");
        }
        [Fact]
        public async Task DeleteAsync_ShouldRemoveMovieFromDatabase()
        {
            var movie = new Movie(
               tmdbId: 888,
               title: "Movie to be deleted",
               originalTitle: "Movie to be deleted Original",
               overview: "This movie will be deleted.",
               releaseDate: new DateOnly(2023, 3, 1),
               runtime: 140,
               originalLanguage: "en",
               voteAverage: 6.5m,
               voteCount: 300,
               posterPath: "/deleteposter.jpg",
               backdropPath: "/deletebackdrop.jpg"
           );
            await _repository.AddAsync(movie);
            var savedMovie = await _repository.GetByTmdbIdAsync(888);
            savedMovie.Should().NotBeNull();
            await _repository.DeleteAsync(savedMovie!);
            var deletedMovie = await _repository.GetByTmdbIdAsync(888);
            deletedMovie.Should().BeNull();

        }
    }
}
