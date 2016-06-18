using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class StoreCalendarRepository : AnatoliRepository<StoreCalendar>, IStoreCalendarRepository
    {
          #region Ctors
        public StoreCalendarRepository() : this(new AnatoliDbContext()) { }
        public StoreCalendarRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
