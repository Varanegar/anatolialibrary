using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class LoyaltyCardBatchRepository : AnatoliRepository<LoyaltyCardBatch>, ILoyaltyCardBatchRepository
    {
        #region Ctors
        public LoyaltyCardBatchRepository() : this(new AnatoliDbContext()) { }
        public LoyaltyCardBatchRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
