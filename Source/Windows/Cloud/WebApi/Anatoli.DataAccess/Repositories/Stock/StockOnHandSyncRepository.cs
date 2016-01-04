using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class StockStockOnHandSyncRepository : AnatoliRepository<StockOnHandSync>, IStockOnHandSyncRepository
    {
        #region Ctors
        public StockStockOnHandSyncRepository() : this(new AnatoliDbContext()) { }
        public StockStockOnHandSyncRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
