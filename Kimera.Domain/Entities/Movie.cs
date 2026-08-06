using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kimera.Domain.Entities
{
    public class Movie
    {
        public int Id { get; private set; }
        public int TmdbId { get; private set; }
        public string Title { get; private set; }
        public string OriginalTitle { get; private set; }
        public string Overview { get; private set; }
        public DateOnly ReleaseDate { get; private set; }
        public int Runtime { get; private set; }
        public string OriginalLanguage { get; private set; }
        public decimal VoteAverage { get; private set; }
        public int VoteCount { get; private set; }
        public string PosterPath { get; private set; }
        public string? BackdropPath { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Movie() { }

        public Movie(int tmdbId, string title, string originalTitle, string overview, DateOnly releaseDate, int runtime, string originalLanguage, decimal voteAverage, int voteCount, string posterPath, string? backdropPath)
        {


            Title = title ?? throw new ArgumentNullException(nameof(title));
            OriginalTitle = originalTitle ?? throw new ArgumentNullException(nameof(OriginalTitle));
            Overview = overview ?? throw new ArgumentNullException(nameof(Overview));
            OriginalLanguage = originalLanguage ?? "NF"; //not found                   
            PosterPath = posterPath ?? throw new ArgumentNullException(nameof(posterPath));
                  
                      


            if (tmdbId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(tmdbId), "Invalid TMDb ID.");
            }
            if (runtime <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(runtime), "Invalid runtime.");
            }
            if (voteAverage < 0 || voteAverage > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(voteAverage), "Vote average must be between 0 and 10.");
            }
            if (voteCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(voteCount), "Invalid vote count.");
            }
            if (releaseDate == default)
                throw new ArgumentException("Release date must be provided.", nameof(releaseDate));
            if (releaseDate > DateOnly.FromDateTime(DateTime.UtcNow))
            {
                throw new ArgumentOutOfRangeException(nameof(releaseDate), "Release date cannot be in the future.");
            }
            TmdbId = tmdbId;
            ReleaseDate = releaseDate;
            Runtime = runtime;
            VoteAverage = voteAverage;
            VoteCount = voteCount;
            BackdropPath = backdropPath; // Pode ser nulo, sem validação
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;

        }
        public void UpdateDetails(string overview, string posterPath, string? backdropPath)
        {
            Overview = overview ?? throw new ArgumentNullException(nameof(overview));
            PosterPath = posterPath ?? throw new ArgumentNullException(nameof(posterPath));
            BackdropPath = backdropPath;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
