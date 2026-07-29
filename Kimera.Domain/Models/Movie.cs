using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kimera.Domain.Models
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
        public string BackdropPath { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Movie(int id, int tmdbId, string title, 
            string originalTitle, string overview, DateOnly releaseDate,
            int runtime, string originalLanguage, decimal voteAverage,
            int voteCount, string posterPath, string backdropPath,
            DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            TmdbId = tmdbId;
            Title = title; ;
            OriginalTitle = originalTitle;
            Overview = overview;
            ReleaseDate = releaseDate;
            Runtime = runtime;
            OriginalLanguage = originalLanguage;
            VoteAverage = voteAverage;
            VoteCount = voteCount;
            PosterPath = posterPath;
            BackdropPath = backdropPath; 
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
