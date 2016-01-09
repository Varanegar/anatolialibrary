using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.ViewModels.StockModels
{
    public class StockProductViewModel : BaseViewModel
    {
        public bool IsEnable { get; set; }
        public Guid FiscalYearId { get; set; }
        public Guid StockGuid { get; set; }
        public Guid ProductGuid { get; set; }
        public Guid? ReorderCalcTypeId { get; set; }
        public decimal MinQty { get; set; }
        public decimal ReorderLevel { get; set; }
        public decimal MaxQty { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public decimal QtyPerPack { get; set; }
        public decimal CurrentOnHand { get; set; }
        public ReorderCalcTypeViewModel ReorderCalcTypeInfo { get; set; }
        public Guid? StockProductRequestSupplyTypeId { get; set; }

    }
}
