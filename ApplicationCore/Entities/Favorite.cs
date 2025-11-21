using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities {
    public class Favorite {
        public int MovieId { get; set; }
        public int UserId { get; set; }

        //navigation Properties
        public virtual Movie Movie { get; set; }

        public virtual User User { get; set; }
    }
}
