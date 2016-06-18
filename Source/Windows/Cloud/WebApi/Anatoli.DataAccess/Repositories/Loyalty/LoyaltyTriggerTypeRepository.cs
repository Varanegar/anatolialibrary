using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class LoyaltyTriggerTypeRepository : AnatoliRepository<LoyaltyTriggerType>, ILoyaltyTriggerTypeRepository
    {
        #region Ctors
        public LoyaltyTriggerTypeRepository() : this(new AnatoliDbContext()) { }
        public LoyaltyTriggerTypeRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
