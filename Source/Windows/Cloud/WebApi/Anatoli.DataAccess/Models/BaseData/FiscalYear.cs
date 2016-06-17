using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Anatoli.DataAccess.Models
{
    public class FiscalYear : AnatoliBaseModel
    {
        [StringLength(10)]
        public string FromPdate { get; set; }
        [StringLength(10)]
        public string ToPdate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public virtual ICollection<StockProduct> StockProducts { get; set; }
    }
}
