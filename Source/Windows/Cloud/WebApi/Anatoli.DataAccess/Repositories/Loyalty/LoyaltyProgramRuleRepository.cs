using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class LoyaltyProgramRuleRepository : AnatoliRepository<LoyaltyProgramRule>, ILoyaltyProgramRuleRepository
    {
        #region Ctors
        public LoyaltyProgramRuleRepository() : this(new AnatoliDbContext()) { }
        public LoyaltyProgramRuleRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
