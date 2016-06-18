using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anatoli.ViewModels.LoyaltyModels
{
    public class LoyaltyRuleConditionViewModel : BaseViewModel
    {
        public Guid LoyaltyRuleId { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        public Guid LoyaltyRuleConditionTypeId { get; set; }

    }
}
