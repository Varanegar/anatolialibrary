using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class StockOnHandSyncRepository : AnatoliRepository<StockOnHandSync>, IStockOnHandSyncRepository
    {
        #region Ctors
        public StockOnHandSyncRepository() : this(new AnatoliDbContext()) { }
        public StockOnHandSyncRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
