using Kimera.Domain.Entities;
namespace Kimera.Tests.Domain
{
    public class MovieTests
    {
        [Fact]
        public void Constructor_Should_Initialize_Properties()
        {
            // Arrange
            int tmdbId = 123;
            string title = "Test Movie";
            string originalTitle = "Original Test Movie";
            string overview = "This is a test movie.";
            DateOnly releaseDate = new DateOnly(2023, 1, 1);
            int runtime = 120;
            string originalLanguage = "en";
            decimal voteAverage = 8.5m;
            int voteCount = 1000;
            string posterPath = "/testposter.jpg";
            string? backdropPath = "/testbackdrop.jpg";
            // Act
            var movie = new Movie(tmdbId, title, originalTitle, overview, releaseDate, runtime, originalLanguage, voteAverage, voteCount, posterPath, backdropPath);
            // Assert
            Assert.Equal(tmdbId, movie.TmdbId); 
            Assert.Equal(title, movie.Title);
            Assert.Equal(originalTitle, movie.OriginalTitle);
            Assert.Equal(overview, movie.Overview);
            Assert.Equal(releaseDate, movie.ReleaseDate);
            Assert.Equal(runtime, movie.Runtime);
            Assert.Equal(originalLanguage, movie.OriginalLanguage);
            Assert.Equal(voteAverage, movie.VoteAverage);
            Assert.Equal(voteCount, movie.VoteCount);
            Assert.Equal(posterPath, movie.PosterPath);
            Assert.Equal(backdropPath, movie.BackdropPath);

        }

        [Theory]
        [InlineData("title", null)]
        [InlineData("originalTitle", null)]
        [InlineData("overview", null)]        
        [InlineData("posterPath", null)]
        public void Constructor_Should_Throw_Exception_For_NonNullable_Parameters(string paramName, string? value)
        {
            // Arrange
            int tmdbId = 1;
            string title = "Valid Title";
            string originalTitle = "Valid Original";
            string overview = "Valid Overview";
            DateOnly releaseDate = new DateOnly(2023, 1, 1);
            int runtime = 120;
            string originalLanguage = "en";
            decimal voteAverage = 8.5m;
            int voteCount = 100;
            string posterPath = "/poster.jpg";
            string? backdropPath = null;

            // Act
            switch (paramName)
            {
                case "title":
                    title = (string)value!;
                    break;

                case "originalTitle":
                    originalTitle = (string)value!;
                    break;
                case "overview":
                    overview = (string)value!;
                    break;
                case "posterPath":
                    posterPath = (string)value!;
                    break;
                default:
                    throw new ArgumentException("Invalid parameter name");
            }
            // Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>new Movie(
                tmdbId,
                title,
                originalTitle, 
                overview,
                releaseDate,
                runtime,
                originalLanguage,
                voteAverage,
                voteCount,
                posterPath,
                backdropPath)
            );
        }

        public static IEnumerable<object[]> GetInvalidParameters()
        {
            yield return new object[] { "tmdbId", -1 };
            yield return new object[] { "runtime", -9 };
            
            yield return new object[] { "voteAverage", (decimal)15 };
            yield return new object[] { "releaseDate", new DateOnly(2028, 4, 4) };
        }
        [Theory]
        [MemberData(nameof(GetInvalidParameters))]
        public void Constructor_Should_Throw_Exception_For_Invalid_Parameters(string paramName, object? value)
        {
            // Arrange
            int tmdbId = 1;
            string title = "Valid Title";
            string originalTitle = "Valid Original";
            string overview = "Valid Overview";
            DateOnly releaseDate = new DateOnly(2023, 1, 1);
            int runtime = 120;
            string originalLanguage = "en";
            decimal voteAverage = 8.5m;
            int voteCount = 100;
            string posterPath = "/poster.jpg";
            string? backdropPath = null;
            // Act
            switch (paramName)
            {
                case "tmdbId":
                    tmdbId = (int)value!;
                    break;
                case "runtime":
                    runtime = (int)value!;
                    break;
                case "voteAverage":
                    voteAverage = (decimal)value!;
                    break;
                case "releaseDate":
                    releaseDate = (DateOnly)value!;
                    break;
                default:
                    throw new ArgumentException("Invalid parameter name");
            }
            // Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new Movie(
                tmdbId,
                title,
                originalTitle,
                overview,
                releaseDate,
                runtime,
                originalLanguage,
                voteAverage,
                voteCount,
                posterPath,
                backdropPath)
            );
        }
        [Fact]
        public void Movie_Should_Have_Default_Language()
        {
            // Arrange
            int tmdbId = 123;
            string title = "Test Movie";
            string originalTitle = "Original Test Movie";
            string overview = "This is a test movie.";
            DateOnly releaseDate = new DateOnly(2023, 1, 1);
            int runtime = 120;
            string originalLanguage = null;
            decimal voteAverage = 8.5m;
            int voteCount = 1000;
            string posterPath = "/testposter.jpg";
            string? backdropPath = "/testbackdrop.jpg";
            // Act
            var movie = new Movie (tmdbId, title, originalTitle, overview, releaseDate, runtime, originalLanguage, voteAverage, voteCount, posterPath, backdropPath);
            // Assert
            Assert.Equal(tmdbId, movie.TmdbId);
            Assert.Equal(title, movie.Title);
            Assert.Equal(originalTitle, movie.OriginalTitle);
            Assert.Equal(overview, movie.Overview);
            Assert.Equal(releaseDate, movie.ReleaseDate);
            Assert.Equal(runtime, movie.Runtime);
            Assert.Equal("NF", movie.OriginalLanguage);
            Assert.Equal(voteAverage, movie.VoteAverage);
            Assert.Equal(voteCount, movie.VoteCount);
            Assert.Equal(posterPath, movie.PosterPath);
            Assert.Equal(backdropPath, movie.BackdropPath);

        }      


    }
}
