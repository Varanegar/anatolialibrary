using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Anatoli.Common.DataAccess.Models;

namespace Anatoli.DataAccess.Models
{
    public class LoyaltyActionType : BaseModel
    {
        [StringLength(100)]
        public string LoyaltyActionTypeName { get; set; }
        public virtual ICollection<LoyaltyRuleAction> LoyaltyRuleActions { get; set; }
    }
}
