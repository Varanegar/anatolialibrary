using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.ViewModels.StoreModels
{
    public class StoreActivePriceListViewModel : BaseViewModel
    {
        public Guid StoreGuid { get; set; }
        public Guid ProductGuid { get; set; }
        public decimal Price { get; set; }

    }
}
