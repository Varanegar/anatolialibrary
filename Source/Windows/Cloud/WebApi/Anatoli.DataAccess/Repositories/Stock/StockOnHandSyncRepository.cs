using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class StockOnHandSyncRepository : AnatoliRepository<StockOnHandSync>, IStockOnHandSyncRepository
    {
        #region Ctors
        public StockOnHandSyncRepository() : this(new AnatoliDbContext()) { }
        public StockOnHandSyncRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
