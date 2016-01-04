using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class StockProductRequestStatusRepository : AnatoliRepository<StockProductRequestStatus>, IStockProductRequestStatusRepository
    {
        #region Ctors
        public StockProductRequestStatusRepository() : this(new AnatoliDbContext()) { }
        public StockProductRequestStatusRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
