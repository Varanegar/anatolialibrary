using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class StockProductRequestProductDetailRepository : AnatoliRepository<StockProductRequestProductDetail>, IStockProductRequestProductDetailRepository
    {
        #region Ctors
        public StockProductRequestProductDetailRepository() : this(new AnatoliDbContext()) { }
        public StockProductRequestProductDetailRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
