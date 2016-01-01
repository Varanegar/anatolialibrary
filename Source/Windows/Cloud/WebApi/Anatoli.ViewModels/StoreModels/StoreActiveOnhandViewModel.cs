using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.ViewModels.StoreModels
{
    public class StoreActiveOnhandViewModel : BaseViewModel
    {
        public Guid StoreGuid { get; set; }
        public Guid ProductGuid { get; set; }
        public decimal Qty { get; set; }

    }
}
