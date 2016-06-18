using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class StoreRepository : AnatoliRepository<Store>, IStoreRepository
    {
          #region Ctors
        public StoreRepository() : this(new AnatoliDbContext()) { }
        public StoreRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
