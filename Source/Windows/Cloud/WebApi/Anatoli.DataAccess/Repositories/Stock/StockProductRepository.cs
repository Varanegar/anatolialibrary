using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class StockProductRepository : AnatoliRepository<StockProduct>, IStockProductRepository
    {
        #region Ctors
        public StockProductRepository() : this(new AnatoliDbContext()) { }
        public StockProductRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
