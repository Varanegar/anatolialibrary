using System;
using Anatoli.ViewModels.User;
using System.Collections.Generic;
using Anatoli.ViewModels.StockModels;
using Anatoli.ViewModels.ProductModels;

namespace Anatoli.ViewModels
{
    public class AccountRequestModel : BaseRequestModel
    {
        public string email { get; set; }

    }
}
