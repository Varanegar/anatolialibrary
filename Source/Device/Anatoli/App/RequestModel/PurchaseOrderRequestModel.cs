using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.RequestModel
{
    public class PurchaseOrderRequestModel : BaseRequestModel
    {
        public string customerId { get; set; }
        public string centerId { get; set; }
        public string poId { get; set; }
    }
}
