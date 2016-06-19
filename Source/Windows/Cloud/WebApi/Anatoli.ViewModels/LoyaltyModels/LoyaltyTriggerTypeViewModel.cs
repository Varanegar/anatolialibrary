using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Anatoli.ViewModels;

namespace Loyalty.DataAccess.Models
{
    //Historical, PerTransaction
    public class LoyaltyTriggerTypeViewModel : BaseViewModel
    {
        public string LoyaltyTriggerTypeName { get; set; }
        public List<LoyaltyRule> LoyaltyRules { get; set; }
    }
}
