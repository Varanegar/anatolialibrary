using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class StockProductRequestRepository : AnatoliRepository<StockProductRequest>, IStockProductRequestRepository
    {
        #region Ctors
        public StockProductRequestRepository() : this(new AnatoliDbContext()) { }
        public StockProductRequestRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
