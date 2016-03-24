using System;
using Anatoli.ViewModels.User;
using System.Collections.Generic;
using Anatoli.ViewModels.StockModels;
using Anatoli.ViewModels.ProductModels;
using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.CustomerModels;

namespace Anatoli.ViewModels
{
    public class ItemImageRequestModel : BaseRequestModel
    {
        public string token { get; set; }
        public string imagetype { get; set; }
        public string imageId { get; set; }
        public bool isDefault { get; set; }
    }
}
