using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class StockRepository : AnatoliRepository<Stock>, IStockRepository
    {
        #region Ctors
        public StockRepository() : this(new AnatoliDbContext()) { }
        public StockRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
