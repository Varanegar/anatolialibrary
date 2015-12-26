using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

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
