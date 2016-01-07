using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DataAccess.Models
{
    public class StockProductRequestStatus : BaseModel
    {
        public string StockProductRequestStatusName { get; set; }
        public virtual ICollection<StockProductRequest> StockProductRequests { get; set; }
    }
}
