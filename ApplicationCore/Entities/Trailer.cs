using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities {
    public class Trailer {
        public int Id { get; set; }
        [MaxLength(256)]
        public string TrailerUrl { get; set; }
        [MaxLength(256)]
        public string Name { get; set; }
        //Reference to movie Id: Foreign Key
        [ForeignKey("Movies")]
        public int MovieId { get; set; }
        //navigation property
        public virtual Movie Movie { get; set; }
    }
}
