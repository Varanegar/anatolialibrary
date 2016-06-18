using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class LoyaltyActionTypeRepository : AnatoliRepository<LoyaltyActionType>, ILoyaltyActionTypeRepository
    {
        #region Ctors
        public LoyaltyActionTypeRepository() : this(new AnatoliDbContext()) { }
        public LoyaltyActionTypeRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
