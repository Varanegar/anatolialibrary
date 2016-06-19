using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anatoli.ViewModels.LoyaltyModels
{
    public class LoyaltyRuleActionViewModel : BaseViewModel
    {
        public decimal ActionValue { get; set; }

        public Guid LoyaltyRuleId { get; set; }
        public string LoyaltyRuleName { get; set; }
        
        public Guid LoyaltyActionTypeId { get; set; }
        public string LoyaltyActionTypeName { get; set; }
        
        public Guid LoyaltyValueTypeId { get; set; }
        public string LoyaltyValueTypeName { get; set; }



    }
}
