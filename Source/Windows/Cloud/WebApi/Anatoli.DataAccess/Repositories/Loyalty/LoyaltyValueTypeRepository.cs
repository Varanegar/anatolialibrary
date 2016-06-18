using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class LoyaltyValueTypeRepository : AnatoliRepository<LoyaltyValueType>, ILoyaltyValueTypeRepository
    {
        #region Ctors
        public LoyaltyValueTypeRepository() : this(new AnatoliDbContext()) { }
        public LoyaltyValueTypeRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
