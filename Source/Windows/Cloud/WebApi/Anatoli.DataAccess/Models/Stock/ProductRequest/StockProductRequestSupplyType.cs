using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DataAccess.Models
{
    public class StockProductRequestSupplyType : AnatoliBaseModel
    {
        [StringLength(100)]
        public string StockProductRequestSupplyTypeName { get; set; }
        public virtual ICollection<StockProductRequest> StockProductRequests { get; set; }
        public virtual ICollection<StockProduct> StockProducts { get; set; }
    }
}
