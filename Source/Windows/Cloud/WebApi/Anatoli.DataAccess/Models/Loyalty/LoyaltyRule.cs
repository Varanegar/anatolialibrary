using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Anatoli.Common.DataAccess.Models;

namespace Anatoli.DataAccess.Models
{
    public class LoyaltyRule : BaseModel
    {
        [StringLength(100)]
        public string LoyaltyRuleName { get; set; }
        [StringLength(2000)]
        public string Description{ get; set; }
        public int CalcPriority { get; set; }
        public virtual ICollection<LoyaltyRuleAction> LoyaltyRuleActions { get; set; }
        public virtual ICollection<LoyaltyProgramRule> LoyaltyProgramRules { get; set; }
        [ForeignKey("LoyaltyRuleType")]
        public Guid LoyaltyRuleTypeId { get; set; }
        [ForeignKey("LoyaltyTriggerType")]
        public Guid LoyaltyTriggerTypeId { get; set; }
        public virtual LoyaltyTriggerType LoyaltyTriggerType { get; set; }
        public virtual LoyaltyRuleType LoyaltyRuleType { get; set; }


    }
}
