using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.ViewModels.StoreModels
{
    public class StoreActiveOnhandViewModel : BaseViewModel
    {
        public string StoreGuid { get; set; }
        public string ProductGuid { get; set; }
        public decimal Qty { get; set; }

    }
}
