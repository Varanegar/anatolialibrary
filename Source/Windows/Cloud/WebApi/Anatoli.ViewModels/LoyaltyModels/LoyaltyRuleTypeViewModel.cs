using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Anatoli.ViewModels.LoyaltyModels
{
    //Historical, PerTransaction
    public class LoyaltyRuleTypeViewModel : BaseViewModel
    {
        public string LoyaltyRuleTypeName { get; set; }
        public List<LoyaltyRuleViewModel> LoyaltyRules { get; set; }
    }
}
