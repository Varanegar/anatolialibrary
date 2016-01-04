using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class StockHistoryOnHandRepository : AnatoliRepository<StockHistoryOnHand>, IStockHistoryOnHandRepository
    {
        #region Ctors
        public StockHistoryOnHandRepository() : this(new AnatoliDbContext()) { }
        public StockHistoryOnHandRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
