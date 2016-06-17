using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class StockActiveOnHandRepository : AnatoliRepository<StockActiveOnHand>, IStockActiveOnHandRepository
    {
        #region Ctors
        public StockActiveOnHandRepository() : this(new AnatoliDbContext()) { }
        public StockActiveOnHandRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
