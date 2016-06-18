using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class StoreActiveOnhandRepository : AnatoliRepository<StoreActiveOnhand>, IStoreActiveOnhandRepository
    {
          #region Ctors
        public StoreActiveOnhandRepository() : this(new AnatoliDbContext()) { }
        public StoreActiveOnhandRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
