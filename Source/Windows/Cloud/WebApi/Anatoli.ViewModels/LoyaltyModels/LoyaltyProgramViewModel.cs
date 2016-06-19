using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Anatoli.ViewModels.LoyaltyModels
{
    public class LoyaltyProgramViewModel : BaseViewModel
    {
        public string LoyaltyProgramName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<LoyaltyProgramRuleViewModel> LoyaltyProgramRules { get; set; }
    }
}
