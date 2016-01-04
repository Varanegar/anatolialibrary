using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class StockProductRepository : AnatoliRepository<StockProduct>, IStockProductRepository
    {
        #region Ctors
        public StockProductRepository() : this(new AnatoliDbContext()) { }
        public StockProductRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
