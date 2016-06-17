using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class LoyaltyRuleRepository : AnatoliRepository<LoyaltyRule>, ILoyaltyRuleRepository
    {
        #region Ctors
        public LoyaltyRuleRepository() : this(new AnatoliDbContext()) { }
        public LoyaltyRuleRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
