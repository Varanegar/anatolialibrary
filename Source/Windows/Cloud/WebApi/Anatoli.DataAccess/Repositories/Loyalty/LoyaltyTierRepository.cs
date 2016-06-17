using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class LoyaltyTierRepository : AnatoliRepository<LoyaltyTier>, ILoyaltyTierRepository
    {
        #region Ctors
        public LoyaltyTierRepository() : this(new AnatoliDbContext()) { }
        public LoyaltyTierRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
