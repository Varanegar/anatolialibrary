using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Anatoli.Common.DataAccess.Models;

namespace Anatoli.DataAccess.Models
{
    public class LoyaltyProgram : BaseModel
    {
        [StringLength(100)]
        public string LoyaltyProgramName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual ICollection<LoyaltyProgramRule> LoyaltyProgramRules { get; set; }
    }
}
