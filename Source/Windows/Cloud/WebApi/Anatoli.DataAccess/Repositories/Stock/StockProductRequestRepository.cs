using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class StockProductRequestRepository : AnatoliRepository<StockProductRequest>, IStockProductRequestRepository
    {
        #region Ctors
        public StockProductRequestRepository() : this(new AnatoliDbContext()) { }
        public StockProductRequestRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
