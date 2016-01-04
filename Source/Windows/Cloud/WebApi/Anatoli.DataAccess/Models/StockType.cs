using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anatoli.DataAccess.Models
{
    public class StockType : BaseModel
    {
        public string StockTypeName { get; set; }
        public virtual ICollection<Stock> Products { get; set; }
        public virtual ICollection<StockProductRequest> StockProductRequests { get; set; }
    }
}
