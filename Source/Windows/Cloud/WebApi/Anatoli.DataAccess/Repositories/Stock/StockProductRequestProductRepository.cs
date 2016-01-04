using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class StockProductRequestProductRepository : AnatoliRepository<StockProductRequestProduct>, IStockProductRequestProductRepository
    {
        #region Ctors
        public StockProductRequestProductRepository() : this(new AnatoliDbContext()) { }
        public StockProductRequestProductRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
