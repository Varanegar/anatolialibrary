using System;
using Anatoli.ViewModels.User;
using System.Collections.Generic;
using Anatoli.ViewModels.StockModels;
using Anatoli.ViewModels.ProductModels;
using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.CustomerModels;

namespace Anatoli.ViewModels
{
    public class LoyaltyRequestModel : BaseRequestModel
    {
        public Guid loyaltyCardId { get; set; }

    }
}
