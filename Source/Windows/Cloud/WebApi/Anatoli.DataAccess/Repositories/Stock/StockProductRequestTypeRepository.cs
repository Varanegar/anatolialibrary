using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class StockProductRequestTypeRepository : AnatoliRepository<StockProductRequestType>, IStockProductRequestTypeRepository
    {
        #region Ctors
        public StockProductRequestTypeRepository() : this(new AnatoliDbContext()) { }
        public StockProductRequestTypeRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
