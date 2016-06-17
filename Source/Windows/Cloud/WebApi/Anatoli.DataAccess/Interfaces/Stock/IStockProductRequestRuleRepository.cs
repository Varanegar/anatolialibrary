using Anatoli.DataAccess.Models;
using Anatoli.Common.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Interfaces
{
    public interface IStockProductRequestRuleRepository : IRepository<StockProductRequestRule>
    {
    }
    public interface IStockProductRequestRuleTypeRepository : IRepository<StockProductRequestRuleType>
    {
    }
    public interface IStockProductRequestRuleCalcTypeRepository : IRepository<StockProductRequestRuleCalcType>
    {
    }
}
