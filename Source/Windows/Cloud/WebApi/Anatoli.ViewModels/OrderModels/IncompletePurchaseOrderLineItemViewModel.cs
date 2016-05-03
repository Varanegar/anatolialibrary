using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.Order
{
    public class IncompletePurchaseOrderLineItemViewModel : BaseViewModel
    {
        public decimal Qty { get; set; }
        public Guid? ProductId { get; set; }
        public Guid IncompletePurchaseOrderId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public double ProductRate { get; set; }
        //public string ProductPictureAddress { get; set; }
    }
}
