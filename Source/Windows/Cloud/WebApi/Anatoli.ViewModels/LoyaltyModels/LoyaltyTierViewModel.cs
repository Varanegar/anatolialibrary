using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Anatoli.ViewModels;
using Anatoli.ViewModels.CustomerModels;

namespace Anatoli.ViewModels.LoyaltyModels
{
    public class LoyaltyTier : BaseViewModel
    {
        public string TierName { get; set; }

        //public List<CustomerLoyaltyTierHistory> CustomerLoyaltyTierHistories { get; set; }
        //public List<CustomerViewModel> Customers { get; set; }
    }
}
