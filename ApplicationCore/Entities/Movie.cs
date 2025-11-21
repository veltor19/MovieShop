using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities {
    public class Movie {
        public int Id { get; set; }
        public string? BackdropUrl { get; set; } //nvarchar(max) --> stores unicode char, varchar(max) --> stores non unicode char
        public decimal Budget { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ImdbURL { get; set; }
        public string? OriginalLanguage { get; set; }
        public string? Overview { get; set; }
        public string? PosterUrl { get; set; }
        public decimal? Price { get; set; }
        public DateTime? ReleaseDate { get; set; }

        public decimal Revenue { get; set; }
        public int? RunTime { get; set; }
        public string? TagLine { get; set; }


        public string? Title { get; set; }

        public string? TmdbUrl { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }

        //Navigation Property
        public virtual ICollection<Trailer> Trailers { get; set; }
        public virtual ICollection<MovieGenre> MovieGenres { get; set; }

        public virtual ICollection<MovieCast> MovieCasts { get; set; }

        public virtual ICollection<Favorite> Favorites { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }

    }
}
