using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Anatoli.ViewModels.LoyaltyModels
{
    public class LoyaltyProgram : BaseViewModel
    {
        [StringLength(100)]
        public string LoyaltyProgramName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<LoyaltyProgramRule> LoyaltyProgramRules { get; set; }
    }
}
