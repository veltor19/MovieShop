using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models {
    public class MovieDetailsModel {
        // Movie main fields (matching DB exactly)

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? TagLine { get; set; }
        public string? Overview { get; set; }
        public string? PosterUrl { get; set; }
        public string? BackdropUrl { get; set; }
        public string? ImdbUrl { get; set; }
        public string? TmdbUrl { get; set; }
        public string? OriginalLanguage { get; set; }
        public decimal Budget { get; set; }
        public decimal Revenue { get; set; }
        public decimal? Price { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? RunTime { get; set; }

        public bool IsPurchased { get; set; }
        public decimal Rating { get; set; }

        // Navigation Models
        public IEnumerable<GenreModel> Genres { get; set; }
        public IEnumerable<CastModel> Casts { get; set; }
        public IEnumerable<TrailerModel> Trailers { get; set; }
    }
}
