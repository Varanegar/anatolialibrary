using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anatoli.ViewModels.LoyaltyModels
{
    public class LoyaltyProgramRuleViewModel : BaseViewModel
    {
        public Guid LoyaltyRuleId { get; set; }
        public string LoyaltyRuleName { get; set; }

        public Guid LoyaltyProgramId { get; set; }
        public string LoyaltyProgramName { get; set; }

        public bool isActive { get; set; }
    }
}
