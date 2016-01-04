using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DataAccess.Models
{
    public class StockProductRequestType : BaseModel
    {
        public string StockPorductRequestTypeName { get; set; }
        public virtual ICollection<StockProductRequest> StockProductRequests { get; set; }
    }
}
