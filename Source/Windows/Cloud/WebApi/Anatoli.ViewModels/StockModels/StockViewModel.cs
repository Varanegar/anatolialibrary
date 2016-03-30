using System;
using System.Collections.Generic;

namespace Anatoli.ViewModels.StockModels
{
    public class StockViewModel : BaseViewModel
    {
        public int CenterId { get; set; }
        public int StockCode { get; set; }
        public string StockName { get; set; }
        public string Address { get; set; }
        public Guid? StoreId { get; set; }
        public Guid? Accept1ById { get; set; }
        public Guid? Accept2ById { get; set; }
        public Guid? Accept3ById { get; set; }
        public Guid? StockTypeId { get; set; }
        public Guid? MainSCMStockId { get; set; }
        public Guid? RelatedSCMStockId { get; set; }
        public Guid? LatestStockOnHandSyncId { get; set; }

        public List<StockProductViewModel> StockProduct { get; set; }
        public List<StockActiveOnHandViewModel> StockActiveOnHand { get; set; }

        public PricipalViewModel Approver1 { get; set; }
        public PricipalViewModel Approver2 { get; set; }
        public PricipalViewModel Approver3 { get; set; }
        public StockTypeViewModel StockType { get; set; }
        public StockViewModel MainStock { get; set; }
        public StockViewModel RelatedStock { get; set; }

        public StockViewModel()
        {
            StockName = "";
        }
    }
}
