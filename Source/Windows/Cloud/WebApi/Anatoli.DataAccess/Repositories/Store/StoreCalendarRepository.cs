using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

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
