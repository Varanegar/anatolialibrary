using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.ViewModels.StockModels
{
    public class StockProductRequestProductViewModel : BaseViewModel
    {
        public decimal RequestQty { get; set; }
        public decimal Accepted1Qty { get; set; }
        public decimal Accepted2Qty { get; set; }
        public decimal Accepted3Qty { get; set; }
        public decimal DeliveredQty { get; set; }
        public Guid ProductId { get; set; }
        public Guid StockProductRequestId { get; set; }
        public List<StockProductRequestProductDetailViewModel> StockProductRequestProductDetails { get; set; }
    }
}
