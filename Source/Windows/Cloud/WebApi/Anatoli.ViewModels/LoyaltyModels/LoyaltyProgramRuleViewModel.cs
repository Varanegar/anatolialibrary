using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Anatoli.Common.DataAccess.Models;

namespace Loyalty.DataAccess.Models
{
    public class LoyaltyProgramRule : BaseModel
    {
        [ForeignKey("LoyaltyRule")]
        public Guid LoyaltyRuleId { get; set; }
        public virtual LoyaltyRule LoyaltyRule { get; set; }
        [ForeignKey("LoyaltyProgram")]
        public Guid LoyaltyProgramId { get; set; }
        public virtual LoyaltyProgram LoyaltyProgram { get; set; }

        public bool isActive { get; set; }
    }
}
