using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.ViewModels.StockModels
{
    public class StockViewModel : BaseViewModel
    {
        public int CenterId { get; set; }
        public int StockCode { get; set; }
        public string StockName { get; set; }
        public string Address { get; set; }
        public Guid StoreId { get; set; }
        public Guid? Accept1ById { get; set; }
        public Guid? Accept2ById { get; set; }
        public Guid? Accept3ById { get; set; }
        public Guid? StockTypeId { get; set; }
        public Guid? MainSCMStockId { get; set; }
        public Guid? RelatedSCMStockId { get; set; }
        public Guid? LatestStockOnHandSyncId { get; set; }

        public List<StockProductViewModel> StockProduct { get; set; }
        public List<StockActiveOnHandViewModel> StockActiveOnHand { get; set; }
    }
}
