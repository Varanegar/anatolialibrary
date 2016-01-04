using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anatoli.DataAccess.Models
{
    public class FiscalYear : BaseModel
    {
        public string FromPdate { get; set; }
        public string ToPdate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public virtual ICollection<StockProduct> StockProducts { get; set; }
    }
}
