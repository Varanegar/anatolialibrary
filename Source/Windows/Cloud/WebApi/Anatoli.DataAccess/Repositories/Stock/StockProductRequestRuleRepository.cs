using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

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

    public class StockProductRequestRuleTypeRepository : AnatoliRepository<StockProductRequestRuleType>, IStockProductRequestRuleTypeRepository
    {
        #region Ctors
        public StockProductRequestRuleTypeRepository() : this(new AnatoliDbContext()) { }
        public StockProductRequestRuleTypeRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
    public class StockProductRequestRuleCalcTypeRepository : AnatoliRepository<StockProductRequestRuleCalcType>, IStockProductRequestRuleCalcTypeRepository
    {
        #region Ctors
        public StockProductRequestRuleCalcTypeRepository() : this(new AnatoliDbContext()) { }
        public StockProductRequestRuleCalcTypeRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
