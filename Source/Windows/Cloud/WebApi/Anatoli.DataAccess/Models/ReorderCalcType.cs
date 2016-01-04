using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DataAccess.Models
{
    public class ReorderCalcType : BaseModel
    {
        public string ReorderTypeName { get; set; }
        public virtual ICollection<StockProduct> StockProducts { get; set; }
        public virtual ICollection<StockProductRequest> StockProductRequests { get; set; }
    }
}
