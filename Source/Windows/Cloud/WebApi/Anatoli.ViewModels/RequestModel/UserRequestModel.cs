using System;
using Anatoli.ViewModels.User;
using System.Collections.Generic;
using Anatoli.ViewModels.StockModels;
using Anatoli.ViewModels.ProductModels;
using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.CustomerModels;

namespace Anatoli.ViewModels
{
    public class UserRequestModel : BaseRequestModel
    {
        public string username { get; set; }
        public string code { get; set; }
        public string password { get; set; }
    }
}
