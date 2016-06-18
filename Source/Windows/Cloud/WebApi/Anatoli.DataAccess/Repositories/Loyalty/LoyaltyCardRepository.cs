using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class LoyaltyCardRepository : AnatoliRepository<LoyaltyCard>, ILoyaltyCardRepository
    {
        #region Ctors
        public LoyaltyCardRepository() : this(new AnatoliDbContext()) { }
        public LoyaltyCardRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
