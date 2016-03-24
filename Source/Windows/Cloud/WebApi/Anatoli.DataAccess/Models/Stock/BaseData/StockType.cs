using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Anatoli.DataAccess.Models
{
    public class StockType : BaseModel
    {
        [StringLength(100)]
        public string StockTypeName { get; set; }
        public virtual ICollection<Stock> Products { get; set; }
        public virtual ICollection<StockProductRequest> StockProductRequests { get; set; }
    }
}
