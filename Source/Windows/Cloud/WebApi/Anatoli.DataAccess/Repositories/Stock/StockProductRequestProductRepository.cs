using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class StockProductRequestProductRepository : AnatoliRepository<StockProductRequestProduct>, IStockProductRequestProductRepository
    {
        #region Ctors
        public StockProductRequestProductRepository() : this(new AnatoliDbContext()) { }
        public StockProductRequestProductRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
