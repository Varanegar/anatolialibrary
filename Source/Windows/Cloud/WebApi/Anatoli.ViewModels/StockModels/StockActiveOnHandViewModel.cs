using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.ViewModels.StockModels
{
    public class StockActiveOnHandViewModel : BaseViewModel
    {
        public Guid StockGuid { get; set; }
        public Guid ProductGuid { get; set; }
        public decimal Qty { get; set; }

    }
}
