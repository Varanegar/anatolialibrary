using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class StockHistoryOnHandRepository : AnatoliRepository<StockHistoryOnHand>, IStockHistoryOnHandRepository
    {
        #region Ctors
        public StockHistoryOnHandRepository() : this(new AnatoliDbContext()) { }
        public StockHistoryOnHandRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
