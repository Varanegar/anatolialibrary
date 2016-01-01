using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.PMC.ViewModels;

namespace Anatoli.PMC.ViewModels.StoreModels
{
    public class PMCStoreOnhandViewModel : PMCBaseViewModel
    {
        public Guid StoreGuid { get; set; }
        public Guid ProductGuid { get; set; }
        public decimal Qty { get; set; }

    }
}
