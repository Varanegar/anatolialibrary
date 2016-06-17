using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class LoyaltyRuleConditionRepository : AnatoliRepository<LoyaltyRuleCondition>, ILoyaltyRuleConditionRepository
    {
        #region Ctors
        public LoyaltyRuleConditionRepository() : this(new AnatoliDbContext()) { }
        public LoyaltyRuleConditionRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
