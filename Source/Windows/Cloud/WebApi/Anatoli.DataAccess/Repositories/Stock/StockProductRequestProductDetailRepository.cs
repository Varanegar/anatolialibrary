using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class StockProductRequestProductDetailRepository : AnatoliRepository<StockProductRequestProductDetail>, IStockProductRequestProductDetailRepository
    {
        #region Ctors
        public StockProductRequestProductDetailRepository() : this(new AnatoliDbContext()) { }
        public StockProductRequestProductDetailRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
