using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class StockProductRequestRuleRepository : AnatoliRepository<StockProductRequestRule>, IStockProductRequestRuleRepository
    {
        #region Ctors
        public StockProductRequestRuleRepository() : this(new AnatoliDbContext()) { }
        public StockProductRequestRuleRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
