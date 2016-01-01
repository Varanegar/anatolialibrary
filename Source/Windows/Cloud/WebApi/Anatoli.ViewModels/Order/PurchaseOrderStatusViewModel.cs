using System;
using System.Linq;
using System.Collections.Generic;

namespace Anatoli.ViewModels.OrderModels
{
    public class OrderStatusViewModel : BaseViewModel
    {
        public Guid InvoiceStatusId { get; set; }
        public DateTime ActionDate { get; set; }
        public string Comment { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoicePDate { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal PayableAmount { get; set; }
        public decimal SettlementAmount { get; set; }
    }
}
