using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ApplicationCore.Entities {
    public class Purchase {
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public DateTime PurchaseDateTime { get; set; }
        public string PurchaseNumber {  get; set; } = string.Empty;

        public decimal? TotalPrice { get; set; }

        //Navigation Properties
        public virtual User User { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
