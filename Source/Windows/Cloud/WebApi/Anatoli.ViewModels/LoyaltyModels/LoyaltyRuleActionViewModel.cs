using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Anatoli.Common.DataAccess.Models;

namespace Loyalty.DataAccess.Models
{
    public class LoyaltyRuleAction : BaseModel
    {
        public decimal ActionValue { get; set; }
        [ForeignKey("LoyaltyRule")]
        public Guid LoyaltyRuleId { get; set; }
        [ForeignKey("LoyaltyActionType")]
        public Guid LoyaltyActionTypeId { get; set; }
        [ForeignKey("LoyaltyValueType")]
        public Guid LoyaltyValueTypeId { get; set; }
        public virtual LoyaltyValueType LoyaltyValueType { get; set; }
        public virtual LoyaltyActionType LoyaltyActionType { get; set; }
        public virtual LoyaltyRule LoyaltyRule { get; set; }

    }
}
