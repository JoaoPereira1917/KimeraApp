using System.Text.Json.Serialization;
namespace Kimera.Application.DTOs
{
    public class MovieDTO
    {
        [JsonPropertyName("id")]
        public int TmdbId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string OriginalTitle { get; set; } = string.Empty;
        public string Overview { get; set; } = string.Empty;
        public DateOnly ReleaseDate { get; set; }
        public int Runtime { get; set; }
        public string OriginalLanguage { get; set; } = string.Empty;
        public decimal VoteAverage { get; set; }
        public int VoteCount { get; set; }
        public string PosterPath { get; set; } = string.Empty;
        public string? BackdropPath { get; set; }
    }
}
