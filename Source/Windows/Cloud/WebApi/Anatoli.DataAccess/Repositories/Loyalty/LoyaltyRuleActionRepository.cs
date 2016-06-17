using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class LoyaltyRuleActionRepository : AnatoliRepository<LoyaltyRuleAction>, ILoyaltyRuleActionRepository
    {
        #region Ctors
        public LoyaltyRuleActionRepository() : this(new AnatoliDbContext()) { }
        public LoyaltyRuleActionRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
