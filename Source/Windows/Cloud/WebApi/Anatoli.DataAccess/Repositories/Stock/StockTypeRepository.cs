using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

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
