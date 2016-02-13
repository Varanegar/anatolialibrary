using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;

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
