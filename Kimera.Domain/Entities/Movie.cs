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
        public decimal VoteAverage  { get; private set; }
        public int VoteCount { get; private set; }
        public string PosterPath { get; private set; }
        public string? BackdropPath { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Movie() { }

        public Movie(int tmdbId, string title, string originalTitle, string overview, DateOnly releaseDate, int runtime, string originalLanguage, decimal voteAverage, int voteCount, string posterPath, string? backdropPath)
        {
            
            TmdbId = tmdbId;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            OriginalTitle = originalTitle?? throw new ArgumentNullException(nameof(OriginalTitle));
            Overview = overview ?? throw new ArgumentNullException(nameof(Overview));
            ReleaseDate = releaseDate;
            Runtime = runtime;
            OriginalLanguage = originalLanguage ??  "NF"; //not found
            VoteAverage = voteAverage;
            VoteCount = voteCount;
            PosterPath = posterPath ?? throw new ArgumentNullException(nameof(posterPath));
            BackdropPath = backdropPath;
            
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
