
using Kimera.Domain.Common;
using System.Dynamic;

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
            // GuardClauses 
            GuardClauses.IsNOtNullOrEmpty(title, nameof(title));
            GuardClauses.IsNOtNullOrEmpty(originalTitle, nameof(originalTitle));
            GuardClauses.IsNOtNullOrEmpty(overview, nameof(overview));
            GuardClauses.IsNOtNullOrEmpty(posterPath, nameof(posterPath));

            // Not Found 
            OriginalLanguage = originalLanguage ?? "NF";

            // Valida números inteiros e ranges
            GuardClauses.IsSmallerOrEqualThanZero(tmdbId, nameof(tmdbId));
            GuardClauses.IsSmallerOrEqualThanZero(runtime, nameof(runtime));
            GuardClauses.IsInvalidRange(voteAverage, nameof(voteAverage), 0m, 10m);
            GuardClauses.IsSmallerThanZero(voteCount, nameof(voteCount));

            if (releaseDate == default)
                throw new ArgumentException("Release date must be provided.", nameof(releaseDate));
            if (releaseDate > DateOnly.FromDateTime(DateTime.UtcNow))
            {
                throw new ArgumentOutOfRangeException(nameof(releaseDate), "Release date cannot be in the future.");
            }

            // Atribuições após validação
            Title = title;
            OriginalTitle = originalTitle;
            Overview = overview;
            TmdbId = tmdbId;
            ReleaseDate = releaseDate;
            Runtime = runtime;
            VoteAverage = voteAverage;
            VoteCount = voteCount;
            PosterPath = posterPath;
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
