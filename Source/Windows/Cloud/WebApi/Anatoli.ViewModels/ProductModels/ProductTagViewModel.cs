using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.ViewModels.ProductModels
{
    public class ProductTagViewModel : BaseViewModel
    {
        public static readonly Guid Special = Guid.Parse("17F36FE2-BFB4-4E7E-BC10-6AE7512750B3");

        public string ProductTagName { get; set; }

    }
}
