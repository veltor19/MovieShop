using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ApplicationCore.Validators;

namespace ApplicationCore.Entities {
    public class Purchase {
        public int MovieId { get; set; }
        public int UserId { get; set; }
        [Required]
        [NotPastDate]
        public DateTime PurchaseDateTime { get; set; }
        public string PurchaseNumber {  get; set; } = string.Empty;

        public decimal? TotalPrice { get; set; }

        //Navigation Properties
        public virtual User User { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
