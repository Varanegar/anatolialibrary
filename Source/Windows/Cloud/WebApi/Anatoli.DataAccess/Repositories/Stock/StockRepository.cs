using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class StockRepository : AnatoliRepository<Stock>, IStockRepository
    {
        #region Ctors
        public StockRepository() : this(new AnatoliDbContext()) { }
        public StockRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
