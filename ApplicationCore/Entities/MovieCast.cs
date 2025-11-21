using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities {
    public class MovieCast {
        public int CastId { get; set; }
        public string Character { get; set; }

        public int MovieId { get; set; }

        //Navigation Properties
        public virtual Cast Cast { get; set; }

        public virtual Movie Movie { get; set; }

    }
}
