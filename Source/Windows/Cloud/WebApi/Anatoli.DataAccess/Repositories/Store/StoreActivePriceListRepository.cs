using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class StoreActivePriceListRepository : AnatoliRepository<StoreActivePriceList>, IStoreActivePriceListRepository
    {
          #region Ctors
        public StoreActivePriceListRepository() : this(new AnatoliDbContext()) { }
        public StoreActivePriceListRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
