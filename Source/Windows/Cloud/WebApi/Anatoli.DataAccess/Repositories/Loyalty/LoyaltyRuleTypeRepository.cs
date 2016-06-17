using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class LoyaltyRuleTypeRepository : AnatoliRepository<LoyaltyRuleType>, ILoyaltyRuleTypeRepository
    {
        #region Ctors
        public LoyaltyRuleTypeRepository() : this(new AnatoliDbContext()) { }
        public LoyaltyRuleTypeRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
