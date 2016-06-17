using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class StockProductRequestStatusRepository : AnatoliRepository<StockProductRequestStatus>, IStockProductRequestStatusRepository
    {
        #region Ctors
        public StockProductRequestStatusRepository() : this(new AnatoliDbContext()) { }
        public StockProductRequestStatusRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
