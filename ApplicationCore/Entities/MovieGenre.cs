using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities {
    public class MovieGenre {
        public int GenreId { get; set; }
        public int MovieId { get; set; }

        //Navigation Property
        public virtual Genre Genre { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
