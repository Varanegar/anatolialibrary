using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anatoli.ViewModels.LoyaltyModels
{
    public class LoyaltyRuleViewModel : BaseViewModel
    {
        public string LoyaltyRuleName { get; set; }
        public string Description{ get; set; }
        public int CalcPriority { get; set; }
        public List<LoyaltyRuleActionViewModel> LoyaltyRuleActions { get; set; }
        public List<LoyaltyProgramRuleViewModel> LoyaltyProgramRules { get; set; }
        public Guid LoyaltyRuleTypeId { get; set; }
        public Guid LoyaltyTriggerTypeId { get; set; }


    }
}
