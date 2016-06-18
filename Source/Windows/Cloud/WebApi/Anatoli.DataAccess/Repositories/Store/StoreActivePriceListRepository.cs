using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class StoreActivePriceListRepository : AnatoliRepository<StoreActivePriceList>, IStoreActivePriceListRepository
    {
          #region Ctors
        public StoreActivePriceListRepository() : this(new AnatoliDbContext()) { }
        public StoreActivePriceListRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
