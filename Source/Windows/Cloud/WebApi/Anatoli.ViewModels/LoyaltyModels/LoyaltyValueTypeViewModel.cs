using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Anatoli.Common.DataAccess.Models;

namespace Loyalty.DataAccess.Models
{
    public class LoyaltyValueType : BaseModel
    {
        [StringLength(100)]
        public string LoyaltyValueTypeName { get; set; }
        public virtual ICollection<LoyaltyRuleAction> LoyaltyRuleActions { get; set; }
    }
}
