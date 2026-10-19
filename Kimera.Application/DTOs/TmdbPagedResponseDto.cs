using System;
using System.Collections.Generic;
using System.Text;

namespace Kimera.Application.DTOs
{
    public class TmdbPagedResponseDto<T>
    {
        public int Page { get; set; }
        public List<T> Results { get; set; } = [];
        public int TotalPages { get; set; }
        public int TotalResults
        {
            get; set;
        }
    }
}
