using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class LoyaltyRuleConditionTypeRepository : AnatoliRepository<LoyaltyRuleConditionType>, ILoyaltyRuleConditionTypeRepository
    {
        #region Ctors
        public LoyaltyRuleConditionTypeRepository() : this(new AnatoliDbContext()) { }
        public LoyaltyRuleConditionTypeRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
