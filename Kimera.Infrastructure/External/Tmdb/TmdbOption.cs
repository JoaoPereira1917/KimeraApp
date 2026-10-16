using System;
using System.Collections.Generic;
using System.Text;

namespace Kimera.Infrastructure.External.Tmdb
{
    public class TmdbOption
    {
        public const string SectionName = "TMDB";

        public string BaseUrl { get; set; } = string.Empty;
        public string BearerToken { get; set; } = string.Empty;
        public string ApiKey{ get; set; } = string.Empty;
    }
}
