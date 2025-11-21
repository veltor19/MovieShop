using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models {
    public class CreateMovieViewModel {
        
        public string Title { get; set; }

       
        public string? Overview { get; set; }

        public string? TagLine { get; set; }

        public decimal Budget { get; set; }

        public decimal Revenue { get; set; }

        public string? ImdbURL { get; set; }

        public string? TmdbUrl { get; set; }

        public string? PosterUrl { get; set; }

        public string? BackdropUrl { get; set; }

        public string? OriginalLanguage { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public int? RunTime { get; set; }

        public decimal? Price { get; set; }
    }
}
