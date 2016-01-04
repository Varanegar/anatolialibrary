using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class StockActiveOnHandRepository : AnatoliRepository<StockActiveOnHand>, IStockActiveOnHandRepository
    {
        #region Ctors
        public StockActiveOnHandRepository() : this(new AnatoliDbContext()) { }
        public StockActiveOnHandRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
