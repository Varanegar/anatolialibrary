using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class StockTypeRepository : AnatoliRepository<StockType>, IStockTypeRepository
    {
        #region Ctors
        public StockTypeRepository() : this(new AnatoliDbContext()) { }
        public StockTypeRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
