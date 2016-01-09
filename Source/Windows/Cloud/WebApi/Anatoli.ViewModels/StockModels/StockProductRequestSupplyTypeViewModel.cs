using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.ViewModels.StockModels
{
    public class StockProductRequestSupplyTypeViewModel : BaseViewModel
    {
        public static readonly Guid SupplyFromSupplier = Guid.Parse("454422E2-8751-4F28-A31D-0C3E8230FA81");
        public static readonly Guid SupplyFromMainStock = Guid.Parse("C6968D05-CFFD-4269-8129-E0F99E63B656");
        public static readonly Guid SupplyFromRelatedStock = Guid.Parse("AB787E71-2120-45D7-9B69-021E60FEA0BB");
        public string StockProductRequestSupplyTypeeName { get; set; }
    }
}
