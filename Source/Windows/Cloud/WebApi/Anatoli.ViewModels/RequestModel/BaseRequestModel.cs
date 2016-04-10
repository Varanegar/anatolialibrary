using System;
using Anatoli.ViewModels.User;
using System.Collections.Generic;
using Anatoli.ViewModels.StockModels;
using Anatoli.ViewModels.ProductModels;

namespace Anatoli.ViewModels
{
    public class BaseRequestModel
    {
        public Guid uniqueId { get; set; }
        public string data { get; set; }
        public string dateAfter { get; set; }
        public string searchTerm { get; set; }
        public string userId { get; set; }
        public string user { get; set; }
    }
}
