using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class StockProductRequestTypeRepository : AnatoliRepository<StockProductRequestType>, IStockProductRequestTypeRepository
    {
        #region Ctors
        public StockProductRequestTypeRepository() : this(new AnatoliDbContext()) { }
        public StockProductRequestTypeRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
