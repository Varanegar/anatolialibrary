using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.Rastak.ViewModels;

namespace Anatoli.Rastak.ViewModels.StoreModels
{
    public class RastakStoreOnhandViewModel : RastakBaseViewModel
    {
        public Guid StoreGuid { get; set; }
        public Guid ProductGuid { get; set; }
        public decimal Qty { get; set; }

    }
}
