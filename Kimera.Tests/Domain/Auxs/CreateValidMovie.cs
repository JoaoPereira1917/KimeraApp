using Kimera.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kimera.Tests.Domain.Auxs
{
    internal static class CreateValidMovie
    {
        public static Movie Create(
        string? titleOverride = null,
        string? originalTitleOverride = null,
        string? overviewOverride = null,
        DateOnly? releaseDateOverride = null,
        int? runtimeOverride = null,
        string? originalLanguageOverride = null,
        decimal? voteAverageOverride = null,
        int? voteCountOverride = null,
        string? posterPathOverride = null,
        string? backdropPathOverride = null)
        {
            // Valores padrão válidos
            int tmdbId = 1;
            string title = titleOverride ?? "Inception";
            string originalTitle = originalTitleOverride ?? "Inception";
            string overview = overviewOverride ?? "A thief who steals corporate secrets...";
            DateOnly releaseDate = releaseDateOverride ?? new DateOnly(2010, 7, 16);
            int runtime = runtimeOverride ?? 148;
            string originalLanguage = originalLanguageOverride ?? "en";
            decimal voteAverage = voteAverageOverride ?? 8.8m;
            int voteCount = voteCountOverride ?? 20000;
            string posterPath = posterPathOverride ?? "/poster.jpg";
            string backdropPath = backdropPathOverride ?? "/backdrop.jpg";

            return new Movie(
                tmdbId: tmdbId,
                title: title,
                originalTitle: originalTitle,
                overview: overview,
                releaseDate: releaseDate,
                runtime: runtime,
                originalLanguage: originalLanguage,
                voteAverage: voteAverage,
                voteCount: voteCount,
                posterPath: posterPath,
                backdropPath: backdropPath
            );
        }

    }
}
