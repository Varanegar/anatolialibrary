using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Anatoli.Common.DataAccess.Models;

namespace Anatoli.DataAccess.Models
{
    public class LoyaltyRuleCondition : BaseModel
    {
        [ForeignKey("LoyaltyRule")]
        public Guid LoyaltyRuleId { get; set; }
        public virtual LoyaltyRule LoyaltyRule { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        [ForeignKey("LoyaltyRuleConditionType")]
        public Guid LoyaltyRuleConditionTypeId { get; set; }
        public virtual LoyaltyRuleConditionType LoyaltyRuleConditionType { get; set; }

    }
}
