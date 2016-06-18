using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Anatoli.Common.DataAccess.Models;

namespace Loyalty.DataAccess.Models
{
    public class LoyaltyRuleConditionType : BaseModel
    {
        [StringLength(100)]
        public string LoyaltyRuleConditionTypeName { get; set; }
        public virtual ICollection<LoyaltyRuleCondition> LoyaltyRuleConditions { get; set; }
    }
}
