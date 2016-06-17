using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class CustomerLoyaltyTierHistoryRepository : AnatoliRepository<CustomerLoyaltyTierHistory>, ICustomerLoyaltyTierHistoryRepository
    {
        #region Ctors
        public CustomerLoyaltyTierHistoryRepository() : this(new AnatoliDbContext()) { }
        public CustomerLoyaltyTierHistoryRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
